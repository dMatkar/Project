function display_kendo_errors(e) {
    if (e.errors) {
        if (typeof (e.errors) === "string") {
            alert(e.errors);
        }
        else {
            let message = "The following errors have occurred:";
            for (let item of e.errors) {
                message += `\n${item}`;
            }
            alert(message);
        }
    }
    else {
        alert("error occurred");
    }
}

function addAntiForgeryToken(data) {
    if (!data)
        data = {};
    let token = $('input[name="__RequestVerificationToken"]').val();
    if (token.length)
        data.__RequestVerificationToken = token;
    return data;
}

function BindSelect2Component(selector, url, additinalData) {
    $(selector).select2({
        minimumInputLength: 3,
        ajax: {
            url: url,
            dataType: "json",
            async: true,
            delay: 150,
            type: "post",
            data: function (params) {
                var query =
                {
                    search: params.term,
                    __RequestVerificationToken: additinalData?.__RequestVerificationToken
                }
                return query;
            },
            processResults: function (data) {
                var result = $.map(data.items, function (obj) {
                    return { id: obj.Id, text: obj.Text };
                });
                return { results: result };
            }
        }
    });
}
