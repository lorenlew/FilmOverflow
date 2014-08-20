(function ($) {
    'use strict';

    //$('#filmModal').on('shown.bs.modal', function () {
    //    $('#fileUpload').fileupload({
    //        dataType: 'json',
    //        url: '/Film/Create',
    //        autoUpload: true,
    //        done: function (e, result) {
    //            if (result.success) {
    //                $('#filmModal').modal('hide');
    //                $(result.replaceTarget).load(result.url); // Load data from the server and place the returned HTML into the matched element
    //            } else {
    //                $('#filmModalContent').html(result);
    //            }                
    //        }
    //    }).on('fileuploadprogressall', function (e, data) {
    //        var progress = parseInt(data.loaded / data.total * 100, 10);
    //        $('.progress .progress-bar').css('width', progress + '%');
    //    });
    //});

    var handleFileSelect = function (evt) {
        var files = evt.target.files;
        var file = files[0];

        if (files && file) {
            var reader = new FileReader();

            reader.onload = function (readerEvt) {
                var binaryString = readerEvt.target.result;
                var test = btoa(binaryString);
                document.getElementById('fileData').text = btoa(binaryString);
                document.getElementById('test22').innerHTML = 'dsfgdfgd';

            };

            reader.readAsBinaryString(file);
        }
    };

    $('#filmModal').on('shown.bs.modal', function () {
        if (window.File && window.FileReader) {
            document.getElementById('filePicker').addEventListener('change', handleFileSelect, false);
        } else {
            alert('The File APIs are not fully supported in this browser.');
        }
    });
    
    $('#filmModal').on('submit', 'form', function (e) {
        e.preventDefault();
        var data = $(this).serialize();

        $.ajax({
            url: this.action,
            type: this.method,
            data: data,
            success: function (result) {
                if (result.success) {
                    $('#filmModal').modal('hide');
                    $(result.replaceTarget).load(result.url); // Load data from the server and place the returned HTML into the matched element
                } else {
                    $('#filmModalContent').html(result);
                }
            }
        });
    });

})(jQuery);