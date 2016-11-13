var searchResult = [];
$(document).ready(function () {
    $('#save-status').hide();
    $('#message').hide();

    $('#save').click(function (e) {

        if ($.isEmptyObject(searchResult)) {
            $('#save-status').html("Unable to save. No search result found.");
            $('#save-status').show();
            return false;
        }

        $.ajax({
            url: '/Search/Save',
            type: "POST",
            dataType: 'json',
            data: JSON.stringify({
                "item": $('#nameToken').val(),
                "location": $('#location').val(),
                "searchResult": [{ "1": "a", "2": "b", "3": "c" }, { "1": "a", "2": "b", "3": "c" }]
            }),
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                $('#save-status').html(data.responseText);
                $('#save-status').show();
            },
            error: function (xhr, status, exception) {
                alert(status);
                alert(exception);                
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
                    alert(searchResult);
                    result += '</tbody>';
                }

                tableBody.html(result);
                $('#loading').hide();
            },
            error: function (xhr, status, exception) {                
                Console.log('Error Test: ' + exception + ', Status: ' + status);
            }
        });
        return false;
    });
});