using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Configuration;
using Project.Core.Domain.Customers;
using Project.Core.Domain.Localization;
using Project.Data;
using Project.Service.Configuration;
using Project.Service.Localization;

namespace Project.Service.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LanguageInsert()
        {
            var repository = new EfRepository<Language>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new LanguageService(repository, new MemoryCacheManager());
            var lang = new Language() { Name = "English", LanguageCulture = "En-Us", FlagImageFileName = "us.png", Published = true, DisplayOrder = 0 };
            service.InsertLanguage(lang);
            Assert.IsTrue(lang.Id != 0);
        }

        [TestMethod]
        public void LanguageUpdate()
        {
            var repository = new EfRepository<Language>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new LanguageService(repository, new MemoryCacheManager());
            var lang = service.GetLanguageById(1);
            lang.DisplayOrder = 1;
            service.UpdateLanguage(lang);
            Assert.IsTrue(lang.DisplayOrder == 1);
        }

        [TestMethod]
        public void ShouldUseCache()
        {
            var repository = new EfRepository<Language>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new LanguageService(repository, new MemoryCacheManager());
            service.GetLanguageById(1);
            service.GetLanguageById(1);
            service.GetLanguageById(1);
            var data = service.GetLanguageById(1);
            Assert.IsTrue(data.Id == 1);
        }

        [TestMethod]
        public void ShouldDeleteData()
        {
            var repository = new EfRepository<Language>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new LanguageService(repository, new MemoryCacheManager());

            var data = service.GetLanguageById(1);
            service.DeleteLanguage(data);
            var list = service.GetAllLanguages();
            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void ShouldInsertLocalStringResources()
        {
            var repository = new EfRepository<LocaleStringResource>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new LocalizationService(repository, new MemoryCacheManager());

            LocaleStringResource localeStringResource = new LocaleStringResource() {LanguageId=2,ResourceName = "Customer.Email", ResourceValue = "Email Address" };
            service.InsertLocalStringResources(localeStringResource);
            Assert.IsTrue(localeStringResource.Id != 0);
        }

        [TestMethod]
        public void ShouldGetLocalStringResouces()
        {
            var repository = new EfRepository<LocaleStringResource>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new LocalizationService(repository, new MemoryCacheManager());

            var  locale = service.GetLocaleStringResources(1);
            Assert.IsTrue(locale.Count() == 3);
        }

        [TestMethod]
        public void ShouldGetLocalStringResoucesFromCache()
        {
            var repository = new EfRepository<LocaleStringResource>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new LocalizationService(repository, new MemoryCacheManager());

           string customerNameValue  = service.GetLocaleStringResource("Customer.Name", 2);
           string customerEmailValue   = service.GetLocaleStringResource("Customer.Email", 2);

            Assert.AreEqual("Customer name", customerNameValue);
            Assert.AreEqual("Email Address",customerEmailValue);
        }

        [TestMethod]
        public void ShouldSaveSettings()
        {
            var repository = new EfRepository<Setting>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new SettingService(repository, new MemoryCacheManager());

            service.SaveSettings(new CustomerSettings() { CustomerEmailEnabled = true, CustomerNameEnabled = true });
            var list = service.GetAllSettings();
            Assert.IsTrue(list.Count() == 2);

            //string data=  service.GetSettingByKey<string>("customersettings.customernameenabled", storeId:0, loadSharedValueIfNotFound: true);
            //Assert.IsTrue(data != null);
            //Assert.IsTrue(data == bool.TrueString);
        }

        [TestMethod]
        public void ShouldLoadSetting()
        {
            var repository = new EfRepository<Setting>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new SettingService(repository, new MemoryCacheManager());

           CustomerSettings customerSettings = service.LoadSettings<CustomerSettings>();
           Assert.IsTrue(customerSettings.CustomerEmailEnabled);
           Assert.IsTrue(customerSettings.CustomerNameEnabled);
        }
        
        [TestMethod]
        public void ShouldUpdateSetting()
        {
            var repository = new EfRepository<Setting>(new ProjectDataContext("Project.Data.ProjectDataContext"));
            var service = new SettingService(repository, new MemoryCacheManager());

           var customerSetting = service.LoadSettings<CustomerSettings>();
            customerSetting.CustomerEmailEnabled = false;
            service.SaveSettings<CustomerSettings>(customerSetting);
            Assert.IsFalse(customerSetting.CustomerEmailEnabled);
        }
    }
}
