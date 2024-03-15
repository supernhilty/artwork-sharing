$(document).ready(function() {
    // Initialize DataTable
    $('#artworkTable').DataTable();

    // Function to fetch data from API and populate the table
    function fetchData() {
        $.ajax({
            url: 'https://localhost:7270/api/admin/artworks?pageNumber=1&pageSize=10',
            type: 'GET',
            success: function(response) {
                console.log(response);
                // Clear existing table data
                $('#artworkTable').DataTable().clear().destroy();

                // Populate table with API data
                $.each(response, function(index, item) {
                    var statusText = item.status ? 'Showing' : 'Not Showing';
                    var links = '<a class="text-capitalize" id="detailsButton" href="' + item.id + '">Details</a>' + ' | ' +
                        '<a class="text-capitalize"  id="statusButton" data-id="' + item.id + '" href="">' + '<button>Change Status</button>'+ '</a>';
                    $('#artworkTable').DataTable().row.add([
                        item.name,
                        item.artist.user.name,
                        item.createdDate,
                        item.description,
                        item.price,
                        statusText,
                        links
                        // Add more data if needed
                    ]).draw();
                });
            },
            error: function(xhr, status, error) {
                // Handle error
                console.error(xhr.responseText);
            }
        });
    }
    // Initial data fetch when the page loads
    fetchData();
    // setInterval(fetchData, 5000);


});

$(document).ready(function () {
    $(document).on('click', '#statusButton', function () {
        // alert($(this).data('id'));
        var id = $(this).data('id');
        
        $.ajax({
            url: 'https://localhost:7270/api/admin/disableArtwork/' + id,
            method: 'put',
            success: function () {
                alert(id + "Has been updated!");
            }
        })
        
    } )
    
})