(function ($) {

    $.fn.makeRequest = function (options) {

        var baseOptions = {
            url: '', // url for make request
            method: '', // method for request
            beforeSave: null, // success callback function when request compelete and success and server returend status code 200
            success: null, // success callback function when request compelete and success and server returend status code 200
            callback: null, // callback function when request finished
            error: null, // error callback function when request has error
            validate: true, // validate form before save
            loader: 2, // 0.None, 1.ElementLoader, 2.BoxLoder, 3.FullscreenLoader
            boxLoader: null,
            entity: null
        };

        baseOptions.url = $(this).attr("action") || $(this).attr("data-save");
        baseOptions.method = $(this).attr("method") || $(this).attr("data-method") || 'POST';
        baseOptions.beforeSave = $(this).attr("data-before-save");
        baseOptions.success = $(this).attr("data-success");
        baseOptions.callback = $(this).attr("data-callback");
        baseOptions.error = $(this).attr("data-error");
        baseOptions.loader = parseInt($(this).attr("loader")) || 0;
        baseOptions.boxLoader = $(this).attr("loader-box") || this;

        var settings = $.extend(baseOptions, options);

        if (!settings.validate || $(this).validate()) {
            var element = this;

            if (settings.loader === 1) {
                var button = $(settings.boxLoader).find("[type=submit]");
                elementLoader(button, true);
                $(settings.boxLoader).find('input, textarea, button, select').prop('disabled', true);
            }
            else if (settings.loader === 2) boxLoader(settings.boxLoader, true);
            else if (settings.loader === 3) loader(true);

            var params = $(element).getEntity();

            if (baseOptions.entity)
                params = baseOptions.entity;

            if (settings.beforeSave) {
                var resultBeforeSave;

                if (typeof settings.beforeSave === "string") {
                    resultBeforeSave = window[settings.beforeSave](params);
                }
                else {
                    resultBeforeSave = settings.beforeSave(params);
                }

                if (resultBeforeSave)
                    params = resultBeforeSave;
            }

            if ($(element).find("[type=file]").length > 0 && $(element).find("[type=file]")[0].files.length > 0) {

                var formData = new FormData();
                console.log(params)
                for (var key in params) {
                    if (params.hasOwnProperty(key)) {
                        if (typeof params[key] != "object") {
                            formData.append(key, params[key]);
                        }
                        else {
                            for (var i = 0; i < params[key].length; i++) {
                                console.log(key + '[' + i + ']', params[key][i]);
                                //formData.append(key + '[' + i + ']', params[key][i]);
                                for (var jKey in params[key][i]) {
                                    var attrName = jKey;
                                    var attrValue = params[key][i][jKey];
                                    formData.append(key + '[' + i + '][' + attrName + ']', attrValue);
                                }
                            }
                        }
                    }
                }

                $(element).find("[type=file]").each(function (index, element) {
                    if ($(this).prop('files').length > 0)
                        formData.append($(this).attr("name"), $(this).prop('files')[0]);
                });

                var request = new XMLHttpRequest();
                request.open(settings.method, baseUrl + settings.url);
                request.onreadystatechange = function () {
                    if (request.readyState == 4) {
                        var button = $("#frmProduct").find("[type=submit]");
                        elementLoader(button, false);
                        $("#frmProduct").find('input, textarea, button, select').prop('disabled', false);
                    }

                    if (request.readyState == 4 && request.status == 200) {

                        var response = JSON.parse(request.responseText);
                        if (response.status === 403 || response.status === 401) {
                            document.location.href = "/login?redirect=" + document.location.pathname;
                            return;
                        }
                        else if (!settings.callback && response.status === 200) {
                            if (settings.success) {
                                if (typeof settings.success === "string") {
                                    window[settings.success](response);
                                }
                                else {
                                    settings.success(response);
                                }
                            }
                        }
                        else if (!settings.callback) {
                            if (settings.error) {
                                if (typeof settings.error === "string") {
                                    window[settings.error](response);
                                }
                                else {
                                    settings.error(response);
                                }
                            }
                            else {
                                alert(response.message);
                            }
                        }

                        if (settings.callback) {
                            if (typeof settings.callback === "string") {
                                window[settings.callback](response);
                            }
                            else {
                                settings.callback(response);
                            }
                        }
                    }
                };

                if (settings.loader === 1) {
                    var button = $(settings.boxLoader).find("[type=submit]");
                    elementLoader(button, false);
                    $(settings.boxLoader).find('input, textarea, button, select').prop('disabled', false);
                }
                else if (settings.loader === 2) boxLoader(element, false);
                else if (settings.loader === 3) loader(false);

                request.send(formData);
            }
            else {
                $.ajax({
                    url: baseUrl + settings.url,
                    type: settings.method,
                    data: settings.method.toUpperCase() === 'GET' ? params : JSON.stringify(params),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {

                        if (settings.loader === 1) {
                            var button = $(settings.boxLoader).find("[type=submit]");
                            elementLoader(button, false);
                            $(settings.boxLoader).find('input, textarea, button, select').prop('disabled', false);
                        }
                        else if (settings.loader === 2) boxLoader(element, false);
                        else if (settings.loader === 3) loader(false);

                        if (response.status === 403 || response.status === 401) {
                            document.location.href = "/login?redirect=" + document.location.pathname;
                            return;
                        }
                        else if (!settings.callback && response.status === 200) {
                            if (settings.success) {
                                if (typeof settings.success === "string") {
                                    window[settings.success](response);
                                }
                                else {
                                    settings.success(response);
                                }
                            }
                        }
                        else if (!settings.callback) {
                            if (settings.error) {
                                if (typeof settings.error === "string") {
                                    window[settings.error](response);
                                }
                                else {
                                    settings.error(response);
                                }
                            }
                            else {
                                alert(response.message);
                            }
                        }

                        if (settings.callback) {
                            if (typeof settings.callback === "string") {
                                window[settings.callback](response);
                            }
                            else {
                                settings.callback(response);
                            }
                        }
                    },
                    error: function () {
                        if (settings.loader === 1) {
                            var button = $(settings.boxLoader).find("[type=submit]");
                            elementLoader(button, false);
                            $(settings.boxLoader).find('input, textarea, button, select').prop('disabled', false);
                        }
                        else if (settings.loader === 2) boxLoader(element, false);
                        else if (settings.loader === 3) loader(false);
                    }
                }).fail(function (jXHR) {
                    if (jXHR.status === 401 || jXHR.status === 403) {
                        document.location.href = "/login?redirect=" + document.location.pathname;
                    }
                    else {
                        alert("در هنگام ارتباط با سرور خطایی رخ داد.");
                    }
                });
            }
        }

        return this;
    };

    $.fn.loadForm = function (options) {

        var baseOptions = {
            url: '', // url for make request
            method: '', // method for request
            callback: null, // callback function when request finished
            success: null, // success callback function when request compelete and success and server returend status code 200
            error: null, // error callback function when request has error
            loader: 2, // 0.None, 1.ElementLoader, 2.BoxLoder, 3.FullscreenLoader
            params: {}
        };

        baseOptions.url = $(this).attr("data-load");
        baseOptions.method = $(this).attr("data-load-method") || 'GET';
        baseOptions.callback = $(this).attr("data-load-callback");
        baseOptions.success = $(this).attr("data-load-success");
        baseOptions.error = $(this).attr("data-load-error");
        baseOptions.loader = parseInt($(this).attr("loader")) || 0;

        var settings = $.extend(baseOptions, options);

        var element = this;
        var id = $(this).find("[name=id]").val();
        if (!id) return;

        if (settings.loader === 1) {
            var button = $(element).find("[type=submit]");
            elementLoader(button, true);
            $(element).find('input, textarea, button, select').prop('disabled', true);
        }
        else if (settings.loader === 2) boxLoader(element, true);
        else if (settings.loader === 3) loader(true);

        $.ajax({
            url: baseUrl + settings.url + "/" + id,
            type: settings.method,
            data: settings.method.toUpperCase() === 'GET' ? settings.params : JSON.stringify(settings.params),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {

                if (settings.loader === 1) {
                    var button = $(element).find("[type=submit]");
                    elementLoader(button, false);
                    $(element).find('input, textarea, button, select').prop('disabled', false);
                }
                else if (settings.loader === 2) boxLoader(element, true);
                else if (settings.loader === 3) loader(true);

                if (response.status === 403 || response.status === 401) {
                    document.location.href = "/login?redirect=" + document.location.pathname;
                    return;
                }
                else if (!settings.callback && response.status === 200) {
                    $(element).setEntity(response.data);

                    if (settings.success) {
                        if (typeof settings.success === "string") {
                            window[settings.success](response);
                        }
                        else {
                            settings.success(response);
                        }
                    }
                }
                else if (!settings.callback) {
                    if (settings.error) {
                        if (typeof settings.error === "string") {
                            window[settings.error](response);
                        }
                        else {
                            settings.error(response);
                        }
                    }
                    else {
                        alert(response.message);
                    }
                }

                if (settings.callback) {
                    if (typeof settings.callback === "string") {
                        window[settings.callback](response);
                    }
                    else {
                        settings.callback(response);
                    }
                }
            },
            error: function () {
                if (settings.loader === 1) {
                    var button = $(element).find("[type=submit]");
                    elementLoader(button, false);
                    $(element).find('input, textarea, button, select').prop('disabled', false);
                }
                else if (settings.loader === 2) boxLoader(element, false);
                else if (settings.loader === 3) loader(false);
            }
        }).fail(function (jXHR) {
            if (jXHR.status === 401 || jXHR.status === 403) {
                document.location.href = "/login?redirect=" + document.location.pathname;
            }
            else {
                alert("در هنگام ارتباط با سرور خطایی رخ داد.");
            }
        });
        return this;
    };

}(jQuery));