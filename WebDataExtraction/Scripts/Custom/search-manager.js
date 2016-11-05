$(document).ready(function () {
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
                var i = 1;
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
                    $.each(data,
                        function (i, item) {
                            i = i + 1;
                            result += '<tr>' +
                                '<td>' + i +'</td>' +
                                '<td>' + item.Name + '</td>' +
                                '<td>' + item.Address + '</td>' +
                                '<td>' + item.Zipcode + '</td>' +
                                '</tr>';
                        });

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