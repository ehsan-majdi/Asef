var baseUrl = '';

$.fn.tagName = function () {
    return this.prop("tagName");
};

$.fn.hasAttr = function (name) {
    var attr = $(this).attr(name);
    // For some browsers, `attr` is undefined; for others,
    // `attr` is false.  Check for both.
    return (typeof attr !== typeof undefined && attr !== false);
};

(function ($) {
    $.fn.getEntity = function (options) {

        var settings = $.extend({
            flat: false
        }, options);

        var entity = getEntity(this, true);

        $(this).find("[data-list]").each(function (intdex, element) {

            var listName = $(element).attr("data-list");
            entity[listName] = [];

            $(element).find("[data-list-item], [data-list-form]").each(function (i, e) {
                var item = getEntity(e, false);
                entity[listName].push(item);
            });
        });

        return entity;
    };

    function getEntity(context, checkParent) {
        var entity = {};

        $(context).find("[name]").each(function (index, element) {

            if (checkParent && $(element).closest("[data-list]").length > 0) {
                return;
            }

            var key = $(element).attr("name");
            var value = "";

            var tagName = $(element).tagName().toLowerCase();
            if (tagName === "input") {
                var type = $(element).attr("type");
                switch (type) {
                    case "text":
                    default:
                        value = $(element).val();

                        if ($(element).hasClass("money-separator"))
                            value = removeSeparator(value);

                        break;
                    case "checkbox":
                        if ($(context).find("input[type=checkbox][name=" + key + "]").length > 1) {
                            value = $(context).find("input[type=checkbox][name=" + key + "]:checked").map(function () {
                                return $(this).val();
                            }).get();
                        }
                        else {
                            value = $(context).find("input[type=checkbox][name=" + key + "]").prop("checked");
                        }
                        break;
                    case "radio":
                        value = $(context).find("input[type=radio][name=" + key + "]:checked").val();
                        break;
                }
            } else if (tagName === "select") {
                if ($(element).attr("multiple")) {
                    value = $(element).val();
                }
                else {
                    value = $(element).find('option:selected').val();
                }
            } else if (tagName === "textarea") {
                value = $(element).val();
            } else {
                var type = $(element).attr("type");
                var format = $(element).attr("format");

                if (type && type.toLowerCase() === "list") {
                    var prefix = "";
                    if ($(element).hasAttr("prefix")) prefix = $(element).attr("prefix");

                    var suffix = "";
                    if ($(element).hasAttr("suffix")) suffix = $(element).attr("suffix");

                    var result = $(element).find("[value]").map(function () {
                        return prefix + $(this).attr("value") + suffix;
                    }).get();

                    if (format && format.toLowerCase() === "string") {
                        value = result.join(",");
                    }
                    else {
                        value = result;
                    }
                }
                else {
                    value = $(element).html();
                }
            }
            entity[key] = value;
        });

        return entity;
    }

    $.fn.setEntity = function (entity, options) {

        var settings = $.extend({
            flat: false
        }, options);

        setEntity(this, entity, true);

        $(this).find("[data-list]").each(function (intdex, element) {

            var listName = $(element).attr("data-list");
            var functionName = $(element).attr("data-function");
            var listData = entity[listName];

            $(element).find("[data-list-item]").remove();
            if (functionName) {
                for (var i = 0; i < listData.length; i++) {
                    window[functionName](listData[i]);
                }
            }

            $(element).find("[data-list-form]").clearEntity();
            $(element).find("[data-list-form]").each(function (i, e) {
                var key = $(this).find("[key]").attr("name");
                var value = $(this).find("[key]").val();
                for (var i = 0; i < listData.length; i++) {
                    if (listData[i][key] == value) {
                        setEntity(e, listData[i], false);
                    }
                }

            });
        });


        return this;
    };

    function setEntity(context, entity, checkParent) {

        $(context).find("[name]").each(function (index, element) {
            var key = $(element).attr("name");
            var value = entity[key];

            if ($(element).hasAttr("lock")) return this;


            if (checkParent && $(element).closest("[data-list]").length > 0) {
                return this;
            }

            var tagName = $(element).tagName().toLowerCase();
            if (tagName === "input") {
                var type = $(element).attr("type");
                switch (type) {
                    case "text":
                    default:
                        $(element).val(value);
                        $(element).change();

                        if ($(element).hasClass("money-separator"))
                            moneySeparator(element);

                        break;
                    case "file":
                        break;
                    case "checkbox":
                    case "radio":
                        var val = $(element).val();
                        $(element).change();
                        if (typeof value === "boolean") {
                            $(element).prop("checked", value).change();
                        }
                        else if (typeof value === "object") {
                            if (value !== null && findInArray(value, val)) {
                                $(element).prop("checked", true).change();
                            } else {
                                $(element).prop("checked", false).change();
                            }
                        }
                        else {
                            if (value !== null && value === val) {
                                $(element).prop("checked", true).change();
                            } else {
                                $(element).prop("checked", false).change();
                            }
                        }
                        break;
                }
            } else if (tagName === "select") {
                if ($(element).attr("multiple")) {
                    $(element).val(value).change();
                }
                else {
                    if (value !== null) {
                        $(element).val(value ? value.toString() : value);
                        $(element).change();
                    }
                    else
                        $(element).val("").change();
                }

            } else if (tagName === "textarea") {
                if (value !== null)
                    $(element).val(value.toString()).change();
                else
                    $(element).val("").change();
            }

            else {
                var type = $(element).attr("type");
                var format = $(element).attr("format");

                if (type && type.toLowerCase() === "list") {

                    var method = $(element).attr("setMethod");

                    var data = value;
                    if (format.toLowerCase() === "string") {
                        data = value ? value.split(",") : [];
                    }
                    else {
                        data = value ? value : []
                    }

                    var prefix = null;
                    if ($(element).hasAttr("prefix")) prefix = $(element).attr("prefix");

                    var suffix = null;
                    if ($(element).hasAttr("suffix")) suffix = $(element).attr("suffix");

                    for (var i = 0; i < data.length; i++) {
                        var item = data[i];
                        if (prefix && item.indexOf(prefix) === 0) {
                            item = item.substring(prefix.length, item.length - prefix.length);
                        }

                        if (suffix && item.lastIndexOf(suffix) === 0) {
                            item = item.substring(0, item.length - item.lastIndexOf(suffix));
                        }

                        window[method](item);
                    }

                }
                else {
                    $(element).html(value);
                }
            }
        });
    }

    $.fn.clearEntity = function (options) {

        var settings = $.extend({
            flat: false
        }, options);

        var context = this;
        $(context).find("[name]").each(function (index, element) {

            if ($(element).hasAttr("lock")) return this;

            var tagName = $(element).tagName().toLowerCase();
            if (tagName === "input") {
                var type = $(element).attr("type");
                switch (type) {
                    case "text":
                    default:
                        $(element).val("").change();
                        break;
                    case "checkbox":
                    case "radio":
                        $(element).prop("checked", false).change();
                        break;
                }
            } else if (tagName === "select") {
                $(element).val($(element).find("option:first").val()).change();
            } else if (tagName === "textarea") {
                $(element).val("").change();
            } else {
                $(element).html("");
            }
            $(element).removeClass("element-error");
        });
        return this;
    };

}(jQuery));

$(document).ready(function () {
    $(document).on("keyup change", ".money-separator", function (e) {
        moneySeparator(this);
    });
});

function findInArray(arr, item) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].toString() === item.toString()) {
            return true;
        }
    }
    return false;
}

function moneySeparator(field) {
    var price;
    price = "";
    while (field.value.indexOf(",") !== -1) {
        field.value = field.value.replace(",", "");
    }
    var txt_field = document.getElementById("ttd");
    txt_field = field.value.length;
    var counter = 0;
    for (var i = txt_field - 1; i >= 0; i--) {
        price = field.value.charAt(i) + price;
        counter++;
        if (counter % 3 === 0 && i > 0) {
            price = "," + price;
            counter = 0;
        }
    }
    field.value = price;
}

function toSeparator(value) {
    if (!value) return "0";

    if (typeof value !== "string") value = value.toString();

    var price = "";
    while (value.indexOf(",") !== -1) {
        value = value.replace(",", "");
    }

    var valueLength = value.length;
    var counter = 0;
    for (var i = valueLength - 1; i >= 0; i--) {
        price = value.charAt(i) + price;
        counter++;
        if (counter % 3 === 0 && i > 0) {
            price = "," + price;
            counter = 0;
        }
    }

    return price;
}

function toSeparatorFa(value) {

    var val = toSeparator(value);
    return toPersianNum(val);
}

function toPersianNum(value) {

    if (typeof value !== "string") value = value.toString();

    value = value.replaceAll("0", "۰")
    value = value.replaceAll("1", "۱")
    value = value.replaceAll("2", "۲")
    value = value.replaceAll("3", "۳")
    value = value.replaceAll("4", "۴")
    value = value.replaceAll("5", "۵")
    value = value.replaceAll("6", "۶")
    value = value.replaceAll("7", "۷")
    value = value.replaceAll("8", "۸")
    value = value.replaceAll("9", "۹")

    return value;
}

function removeSeparator(input) {
    return input.replaceAll(",", "");
}

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};

function getOS() {
    var userAgent = window.navigator.userAgent,
        platform = window.navigator.platform,
        macosPlatforms = ['Macintosh', 'MacIntel', 'MacPPC', 'Mac68K'],
        windowsPlatforms = ['Win32', 'Win64', 'Windows', 'WinCE'],
        iosPlatforms = ['iPhone', 'iPad', 'iPod'],
        os = null;

    if (macosPlatforms.indexOf(platform) !== -1) {
        os = 'Mac OS';
    } else if (iosPlatforms.indexOf(platform) !== -1) {
        os = 'iOS';
    } else if (windowsPlatforms.indexOf(platform) !== -1) {
        os = 'Windows';
    } else if (/Android/.test(userAgent)) {
        os = 'Android';
    } else if (!os && /Linux/.test(platform)) {
        os = 'Linux';
    } else {
        os = 'Other';
    }

    return os;
}

function getBrowser() {
    // Opera 8.0+
    var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
    // Firefox 1.0+
    var isFirefox = typeof InstallTrigger !== 'undefined';
    // At least Safari 3+: "[object HTMLElementConstructor]"
    var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
    // Internet Explorer 6-11
    var isIE = /*@cc_on!@*/false || !!document.documentMode;
    // Edge 20+
    var isEdge = !isIE && !!window.StyleMedia;
    // Chrome 1+
    var isChrome = !!window.chrome && !!window.chrome.webstore;
    // Blink engine detection
    var isBlink = (isChrome || isOpera) && !!window.CSS;

    if (isFirefox) {
        return "Mozilla Firefox";
    } else if (isChrome) {
        return "Google Chrome";
    } else if (isSafari) {
        return "Apple Safari";
    } else if (isEdge) {
        return "Edge";
    } else if (isIE) {
        return "Internet Explorer";
    } else if (isOpera) {
        return "Opera";
    } else {
        return "Other";
    }
}

function deleteEntity(url, callback) {
    confirmMessage("آیا از حذف اطلاعات اطمینان دارید؟", function () {
        boxLoader(".list-form", true);
        backgroundPost(url, null, function (response) {
            boxLoader(".list-form", false);
            callback(response);
        });
    });
}

function backgroundPost(url, params, callback) {
    backgroundAjax(url, 'POST', params, callback);
}

function backgroundGet(url, params, callback) {
    backgroundAjax(url, 'GET', params, callback);
}

function backgroundAjax(url, type, params, callback) {
    $.ajax({
        url: baseUrl + url,
        type: type,
        data: type === 'GET' ? params : JSON.stringify(params),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response.status === 403 || response.status === 401) {
                document.location.href = "/login?redirect=" + document.location.pathname;
            }
            else {
                if (callback)
                    callback(response);
            }
        },
        error: function (data, statusCode, errorThrown) {
            if (data.status === 403) {
                document.location.href = "/login?redirect=" + document.location.pathname;
            }
        }
    });
}

function deleteCookie(name) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

// URL Params
{
    function setParam(key, value) {
        var params = getParam();
        if (typeof key === "string")
            params[key] = value;
        else
            $.extend(params, key);

        var url = window.location.href;
        var pos = url.indexOf('#!');

        if (pos > 0)
            url = url.slice(0, pos);

        window.location.href = url + "#!" + jsonToUrlParams(params);
    }

    function resetParam(key, value) {
        var params = getParam();
        if (typeof key === "string")
            params[key] = value;
        else
            $.extend(params, key);

        var url = window.location.href;
        var pos = url.indexOf('#!');

        if (pos > 0)
            url = url.slice(0, pos);

        window.location.href = url + "#!" + jsonToUrlParams(params);
    }

    function getParam(key) {
        var url = window.location.href;

        var pos = url.indexOf('#!');
        if (pos < 0) return {};

        var vars = {}, hashes = url.slice(pos + 2).split('&');
        for (var i = 0; i < hashes.length; i++) {
            var decode = decodeURIComponent(hashes[i]);
            var hash = decode.split('=');
            vars[hash[0]] = hash.length > 1 ? getValue(hash[1]) : null;
        }

        if (key) {
            return vars[key];
        }
        else
            return vars;
    }

    function getValue(value) {
        if (value.startsWith("[") && value.endsWith("]")) {
            return value.substring(1).substr(0, value.length - 2).split(",");
        }
        else {
            return value;
        }
    }

    function jsonToUrlParams(data) {
        var url = Object.keys(data).map(function (element) {
            if (data[element] !== null && typeof data[element] === "object")
                return encodeURIComponent(element) + '=' + encodeURIComponent("[" + data[element].join(",") + "]");
            else
                return encodeURIComponent(element) + '=' + encodeURIComponent(data[element]);
        }).join('&');
        return url;
    }

    $(document).ready(function () {
        $(document).on("click", ".list-page", function (event) {
            event.preventDefault();
            var page = $(this).attr("data-page");
            setParam("page", page);
        });
    });

}


var loaderTemplate =
    '<div class="box-loader">' +
    '<div class="txt-al-c">' +
    '<div class="dialog-loader-content">' +
    '<div uk-spinner></div>' +
    '</div>' +
    '</div>' +
    '</div>';
var loaderCount = 0;
function loader(status) {
    if (!$("#dialog-loader").length) {
        $("body").append(loaderTemplate);
    }

    if (status) {
        UIkit.modal($("#dialog-loader"), { escClose: false, bgClose: false }).show();
        loaderCount++;
    }
    else {
        if (loaderCount <= 1)
            UIkit.modal($("#dialog-loader")).hide();
        if (loaderCount > 0)
            loaderCount--;
    }
}

function boxLoader(content, status) {
    if ($(content).find('.box-loader').length === 0) {
        $(content).append(loaderTemplate);
    }

    if (status) {
        $(content).find('.box-loader').removeClass("uk-hidden");
    }
    else {
        $(content).find('.box-loader').addClass("uk-hidden");
    }
}

function elementLoader(element, status) {
    var html = "";
    if (status) {
        html = $(element).html();
        $(element).attr("data-html", html);
        $(element).html("<div uk-spinner></div>");
        $(element).addClass("loader");
    }
    else {
        html = $(element).attr("data-html");
        $(element).html(html);
        $(element).removeClass("loader");
    }
}