var searchResult = [];
$(document).ready(function () {    
    $('#message').hide();

    $('#import').click(function (e) {

        if ($.isEmptyObject($('#nameToken').val())) {
            $('#message-text').html("You did not provide a name of the Restaurent.");
            $('#message').show();
            return false;
        }

        if ($.isEmptyObject($('#location').val())) {
            $('#message-text').html("You did not provide a location.");
            $('#message').show();
            return false;
        }

        $('#loading').show();
        $('#message').hide();

        $.ajax({
            url: '/Search/Import',
            type: "POST",
            dataType: 'json',
            data: JSON.stringify({
                "item": $('#nameToken').val(),
                "location": $('#location').val()                
            }),
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                $('#message').hide();

                if (data.success == false) {                    
                    $('#message-text').html(data.responseText);
                    $('#message').show();
                    $('#loading').hide();
                    return false;                    
                }

                var result = "";
                var tableBody = $("#searchResultTable");
                tableBody.html('');

                result += '<thead>' +
                    '<tr>' +
                    '<th>SL.</th>' +
                    '<th>Name</th>' +
                    '<th>Address</th>' +
                    '<th>Zip</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody id="tableBody">';
                if ($.isEmptyObject(data)) {
                    $("#searchResultTable").hide();
                    $('#message-text').html("Sorry, could not import data. No data was found.");
                    $('#message').show();
                } else {                    
                    searchResult = [];
                    $.each(data,
                        function (i, item) {
                            i = i + 1;
                            var name = item.Name;
                            var address = item.Address;
                            var zipcode = item.Zipcode;
                            result += '<tr>' +
                                '<td>' + i + '</td>' +
                                '<td>' + name + '</td>' +
                                '<td>' + address + '</td>' +
                                '<td>' + zipcode + '</td>' +
                                '</tr>';

                            searchResult.push({ name, address, zipcode });
                            $('#message-text').html("Data imported from database successfully.");
                            $('#message').show();
                        });
                    result += '</tbody>';
                }

                tableBody.html(result);
                $("#searchResultTable").show();
                $('#loading').hide();
            },
            error: function (xhr, status, exception) {                                
                $('#message-text').html("Error occured. Could not retrieve data.");
                $('#message').show();
            }
        });
    });

    $('#save').click(function (e) {
        if ($.isEmptyObject(searchResult)) {
            $('#message-text').html("Unable to save. No search result found.");
            $('#message').show();
            return false;
        }

        $('#loading').show();

        $.ajax({
            url: '/Search/Save',
            type: "POST",
            dataType: 'json',
            data: JSON.stringify({
                "item": $('#nameToken').val(),
                "location": $('#location').val(),
                "searchResult": searchResult
            }),
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                $('#loading').hide();
                $('#message-text').html(data.responseText);
                $('#message').show();
            },
            error: function (xhr, status, exception) {
                $('#message-text').html("Error occured. Could not Save data.");
                $('#message').show();                
            }
        });
    });

    $('#searchForm').submit(function (e) {        
        e.preventDefault();        
        $('#loading').show();
        $('#message').hide();

        $.ajax({
            url: '/Search/Get?item=' + $('#nameToken').val() + '&location=' + $('#location').val(),
            type: "GET",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                $('#message').hide();
                
                if (data.success == false) {
                    $('#message-text').html(data.responseText);
                    $('#message').show();
                    $('#loading').hide();
                    return false;
                }

                var result = "";
                var tableBody = $("#searchResultTable");
                tableBody.html('');

                result += '<thead>' +
                    '<tr>' +
                    '<th>SL.</th>' +
                    '<th>Name</th>' +
                    '<th>Address</th>' +
                    '<th>Zip</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody id="tableBody">';
                if ($.isEmptyObject(data)) {
                    $("#searchResultTable").hide();
                    $('#message-text').html("Sorry, we could not find any restaurent in the specified location.");
                    $('#message').show();                    
                } else {
                    $("#searchResultTable").show();
                    searchResult = [];
                    $.each(data,
                        function (i, item) {
                            i = i + 1;
                            var name = item.Name;
                            var address = item.Address;
                            var zipcode = item.Zipcode;
                            result += '<tr>' +
                                '<td>' + i + '</td>' +
                                '<td>' + name + '</td>' +
                                '<td>' + address + '</td>' +
                                '<td>' + zipcode + '</td>' +
                                '</tr>';

                            searchResult.push({ name, address, zipcode });
                        });                    
                    result += '</tbody>';
                }

                tableBody.html(result);
                $('#loading').hide();
            },
            error: function (xhr, status, exception) {
                $('#message-text').html("Error occured. Could not retrieve data.");
                $('#message').show();                
            }
        });
        return false;
    });
});