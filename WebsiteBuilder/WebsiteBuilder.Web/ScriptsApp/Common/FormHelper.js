var FormHelper = (function () {
   

   

    var handleOperationResult = function (result, div) {
        if (!result || !result.ErrorMessages)
            return;

        var msg = '';
        for (var i = 0; i < result.ErrorMessages.length; i++) {
            msg += '<div>' + result.ErrorMessages[i] + '</div>';
        }

        if (msg) {
            if (div)
                div.html(msg);
            else
                FormHelper.alertError(msg);
        }
    };

    var buttonReset = function (btn) {
        $(btn).button('reset');
    };

    var buttonLoading = function (btn) {
        $(btn).button('loading');
    };

    var isNullOrWhitespace = function (input) {
        if (typeof input === 'undefined' || input == null) return true;

        return input.replace(/\s/g, '').length < 1;
    };

    var isElementVisible = function (el) {
        return $(el).is(':visible');
    };

    var isElementDisabled = function (el) {
        return $(el).is(':disabled');
    };

    var setElementVisible = function (el, visible) {
        if (visible) {
            $(el).show();
        } else {
            $(el).hide();
        }
    }

    var setElementDisabled = function (el, disabled) {
        if (disabled) {
            $(el).attr('disabled', 'disabled');
        } else {
            $(el).removeAttr('disabled');
        }
    };

    var clearForm = function (form) {
        $(form).find('input[type="text"],select,textarea').val('');
    };

    var setFormDisabled = function (form, disabled) {
        $(form).find('input[type!="hidden"],select,textarea').each(function (index, element) {
            FormHelper.setElementDisabled(element, disabled);
        });
    };

    var showModal = function (modalId, data, options, initModalFn) {
        modalId = getIdBySelector(modalId);
        var opts = options || {};

        var modalDiv = $('#' + modalId);
        if (modalDiv.length) {
            modalDiv.remove();
            $('.modal-backdrop').remove();
        }

        $('body').append("<div id='" + modalId + "' class='modal fade' tabindex='-1' role='dialog'></div>");
        modalDiv = $('#' + modalId);
        modalDiv.html(data).modal(opts);
        if (initModalFn) {
            initModalFn();
        }
        modalDiv.modal('show');
        modalDiv.on('hidden.bs.modal', function () {
            modalDiv.remove();
        });
    };

    var hideModal = function (modalId) {
        modalId = getIdBySelector(modalId);
        var modalDiv = $('#' + modalId);
        if (modalDiv.length) {
            $(modalDiv).modal('hide');
        }
    };

    var attachEnterPressedHandler = function (element, handler) {
        $(element).on('keypress', function (e) {
            if (e.keyCode === 13) {
                handler.call(element);
            }
        });
    };

    var getFormValues = function (form) {
        var formObj = {};
        var inputs = $(form).serializeArray();

        $.each(inputs, function (i, input) {
            formObj[input.name] = input.value;
        });

        //add checkboxes
        $(form).find('input:checkbox').each(function () {
            formObj[this.name] = this.checked;
        });

        return formObj;
    };

    var setFormValues = function (form, data) {
        $.each(data, function (key, value) {
            var ctrl = $('[name=' + key + ']', form);
            if (ctrl.is('label')) {
                ctrl.text(value);
            } else {
                switch (ctrl.prop("type")) {
                    case "radio":
                    case "checkbox":
                        ctrl.each(function () {
                            if ($(this).attr('value') == value) $(this).attr("checked", value);
                        });
                        break;
                    default:
                        ctrl.val(value);
                }
            }
        });
    }

    var fitVideos = function () {
        $(document).fitVids();
    };

    //dateText - date in format DD.MM.YYYY
    var convertDateForAjaxGet = function (dateText) {
        var momentDate = momentDateFromStringDate(dateText);
        if (!momentDate)
            return null;
        return momentDate.toJSON();
    };
    // dateText - date text in our special format
    // returns date suitable for url
    var convertDateForHttpGet = function (dateText) {
        var momentDate = momentDateFromStringDate(dateText);
        if (!momentDate)
            return null;
        return momentDate.format('YYYY-MM-DD');
    };
    //dateText - date in format DD.MM.YYYY
    var momentDateFromStringDate = function (dateText) {
        if (!dateText)
            return null;
        return moment(dateText, 'DD.MM.YYYY').utc(true);
    }
    //dateText - date in format DD.MM.YYYY HH:mm:ss
    var momentDateFromStringDateTime = function (dateText) {
        if (!dateText)
            return null;
        return moment(dateText, 'DD.MM.YYYY HH:mm:ss').utc(true);
    }
    // momentDate - moment object with date
    var formatMomentDate = function (momentDate) {
        if (!momentDate)
            return null;
        return momentDate.format('DD.MM.YYYY');
    }

    var fromJsonDateToShortDateString = function (jsonDate) {
        return moment(jsonDate).format("YYYY-MM-DD");
    }

    // converts from /Date(1198908717056-0700)/ to // 2013-02-04T18:35:24+02:00
    var convertDateFromJsonToIso8601 = function (jsonDate) {
        if (!jsonDate) {
            return null;
        }
        var dateMomentUtc = moment.utc(jsonDate);
        var localDateIso8601 = dateMomentUtc.local().format();
        return localDateIso8601;
    }

    var alertInfo = function (message, title) {
        $.notify({
            title: title ? '<strong>' + title + '</strong><br>' : '',
            message: message
        },
            {
                type: 'success'
            });
    };

    var alertError = function (message) {
        $.notify({
            message: message
        },
            {
                type: 'danger',
                delay: 0,
                offset: {
                    x: 20,
                    y: 120
                }
            });
    };

    var showConfirmMessage = function (message, yesCallback, noCallback) {
        $.SmartMessageBox({
            title: message,
            buttons: '[Tak][Nie]'
        }, function (buttonPressed) {
            if (buttonPressed == "Tak") {
                if (yesCallback) {
                    yesCallback();
                };
            } else if (noCallback) {
                noCallback();
            }
        });
    }

    var showInputMessage = function (message, yesCallback, noCallback, placeholder) {
        $.SmartMessageBox({
            title: message,
            buttons: '[Tak][Nie]',
            input: "text",
            inputValue: "",
            placeholder: placeholder
        }, function (buttonPressed, value) {
            if (buttonPressed == "Tak") {
                if (yesCallback) {
                    yesCallback(value);
                };
            } else if (noCallback) {
                noCallback();
            }
        });
    }

    var stopEventPropagation = function (e) {
        if (!e)
            e = window.event;

        //IE9 & Other Browsers
        if (e.stopPropagation) {
            e.stopPropagation();
        }
        //IE8 and Lower
        else {
            e.cancelBubble = true;
        }
    };

    function preventDefault(e) {
        // old IE
        if (window.event) {
            if (window.event.preventDefault) {
                window.event.preventDefault();
            } else {
                window.event.returnValue = false;
            }
        }
        // other
        if (e.preventDefault) {
            e.preventDefault();
        } else {
            e.returnValue = false;
        }
    }

    function populateDropDownWithSelectOptions(response, controlId, addEmptyElement, emptyElementDisplayName, emptyElementDisplayValue) {
        $(controlId).html();
        if (addEmptyElement) {
            $(controlId).append($("<option></option>").val(emptyElementDisplayValue).html(emptyElementDisplayName));
        }

        for (var i = 0; i < response.length; i++) {
            var value;
            if (response[i].Id) {
                value = response[i].Id;
            } else {
                value = response[i].Code;
            }
            $(controlId).append($("<option></option>").val(value).html(response[i].Name));
        }
    };

    function getRandomColor() {
        var letters = '0123456789ABCDEF'.split('');
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }

    function digtsOnly(e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    };

    // Email obfuscator script by http://www.jottings.com/obfuscator/
    // Visit above page to generate 'coded' and 'key' parameters
    var deobfuscateEmail = function (coded, key) {

        var shift = coded.length
        var link = ""
        for (i = 0; i < coded.length; i++) {
            if (key.indexOf(coded.charAt(i)) == -1) {
                var ltr = coded.charAt(i)
                link += (ltr)
            }
            else {
                var ltr = (key.indexOf(coded.charAt(i)) - shift + key.length) % key.length
                link += (key.charAt(ltr))
            }
        }
        return link;
    };

    // Generated at http://www.jottings.com/obfuscator/
    var emailObfuscations = {
        infoCoded: 'n453@n-RG3fa.Gt',
        infoKey: 'krcDZotJR8TzOSn9fKjY3aWF70UpM4y5P6X1dBIqLgGeQNC2bmxvHlVshwAuEi',
        marketingCoded: 'TL2WH3XVe@X-oru23.rv',
        marketingKey: 'w15tCbp8NAGkFuHVc9PhxJd3amrlZqIWOSzsgKiU0QEMLT2vD7YRBj4oenXy6f',
    };

    var getDisciplineTextById = function (disciplineId) {
        var result = '';
        $.each(TPEnums.Discipline, function (prop, val) {
            if (val == disciplineId) {
                result = prop;
                return;
            }
        });

        return result;
    }

    var appendDivWithGuid = function (element, guid) {
        var string = TextHelper.format("<div id='{0}'></div>", guid);

        $(element).append(string);
    }

    var checkIfElementExists = function (element) {
        if ($(element).length) {
            return true;
        } else {
            return false;
        }
    }

    var appendSelectorToObjectValues = function (object, selector) {
        for (var propName in object) {
            if (object.hasOwnProperty(propName)) {
                object[propName] = selector + " " + object[propName];
            }
        }
    }

    var getIdBySelector = function (selector) {
        if (selector.charAt(0) == '#') {
            return selector.substring(1);
        }
        return selector;
    }

    function jqueryValidate(formSelector, params) {
        var defaultParams = {
            errorClass: 'invalid',
            errorElement: 'em',
            highlight: function (element) {
                $(element).parent().removeClass('state-success').addClass("state-error");
                $(element).removeClass('valid');
            },
            unhighlight: function (element) {
                $(element).parent().removeClass("state-error").addClass('state-success');
                $(element).addClass('valid');
            },
            errorPlacement: function (error, element) {
                error.insertAfter(element.parent());
            }
        };

        var validateParams = $.extend(true, defaultParams, params);
        $(formSelector).validate(validateParams);
    };

    var createDatePicker = function (selector, options) {
        var opts = $.extend({
            locale: 'pl',
            changeMonth: true,
            changeYear: true,
            format: 'yyyy-mm-dd',
            prevText: '<i class="fa fa-chevron-left"></i>',
            nextText: '<i class="fa fa-chevron-right"></i>'
        }, options);

        $(selector).datepicker(opts);
        $(selector).change(function () {
            //$(this).parsley().validate();
        });
    }

    return {
        getIdBySelector: getIdBySelector,
        handleOperationResult: handleOperationResult,
        buttonReset: buttonReset,
        buttonLoading: buttonLoading,
        isNullOrWhitespace: isNullOrWhitespace,
        isElementVisible: isElementVisible,
        isElementDisabled: isElementDisabled,
        setElementVisible: setElementVisible,
        setElementDisabled: setElementDisabled,
        showModal: showModal,
        hideModal: hideModal,
        attachEnterPressedHandler: attachEnterPressedHandler,
        getFormValues: getFormValues,
        setFormValues: setFormValues,
        fitVideos: fitVideos,
        clearForm: clearForm,
        setFormDisabled: setFormDisabled,
        momentDateFromStringDate: momentDateFromStringDate,
        momentDateFromStringDateTime: momentDateFromStringDateTime,
        formatMomentDate: formatMomentDate,
        convertDateForHttpGet: convertDateForHttpGet,
        convertDateForAjaxGet: convertDateForAjaxGet,
        alertInfo: alertInfo,
        alertError: alertError,
        stopEventPropagation: stopEventPropagation,
        populateDropDownWithSelectOptions: populateDropDownWithSelectOptions,
        digtsOnly: digtsOnly,
        preventDefault: preventDefault,
        deobfuscateEmail: deobfuscateEmail,
        emailObfuscations: emailObfuscations,
        getDisciplineTextById: getDisciplineTextById,
        appendDivWithGuid: appendDivWithGuid,
        checkIfElementExists: checkIfElementExists,
        jqueryValidate: jqueryValidate,
        appendSelectorToObjectValues: appendSelectorToObjectValues,
        fromJsonDateToShortDateString: fromJsonDateToShortDateString,
        showConfirmMessage: showConfirmMessage,
        showInputMessage: showInputMessage,
        convertDateFromJsonToIso8601: convertDateFromJsonToIso8601,
        createDatePicker: createDatePicker,
        getRandomColor: getRandomColor

    }
})();