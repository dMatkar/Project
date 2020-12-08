using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Infrastructure
{
    public class EngineContext
    {
        #region Properties
        /// <summary>
        /// Get the instance of current engine.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize();
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Used to initialize project engine.
        /// </summary>
        /// <param name="forceRecreate"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Initialize(bool forceRecreate = false)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = new ProjectEngine();
                Singleton<IEngine>.Instance.Initialize();
            }
        }

        /// <summary>
        /// Used to replace current engine.
        /// </summary>
        /// <param name="newEngineInstance"></param>
        public static void Replace(IEngine newEngineInstance)
        {
            if (newEngineInstance != null)
            {
                Singleton<IEngine>.Instance = newEngineInstance;
            }
        }

        #endregion
    }
}
