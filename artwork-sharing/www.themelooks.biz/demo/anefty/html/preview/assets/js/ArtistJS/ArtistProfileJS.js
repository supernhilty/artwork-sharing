$(document).ready(function() {
    function fetchData() {
        $.ajax({
            url: 'https://localhost:7270/GetArtistProfile/41dfeaca-fcee-4d24-aab9-289a53219fa0',
            type: 'GET',
            success: function(response) {
                document.getElementById("profile-div")
                    .innerHTML += '</div><a id="requestButton" data-id="'+response.id+'" class="btn btn-primary style--two mr-10 mb-4">' +'<button style="color: white" >Request Service</button>' + '</a></div>';
                
                fetchArtistData(response);
                console.log(response);
 
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


    function fetchArtistData(response) {
        document.getElementById("artist_name").textContent = response.user.name;
        document.getElementById("artist_description").textContent = response.bankAccount;
        document.getElementById("get-link").value = "@" + response.user.normalizedUserName.toLowerCase();
        
    }
    
    
});

$(document).ready(function () {
    $(document).on('click', '#requestButton', function () {
        var artistId = $(this).data('id');
        localStorage.setItem("artistId", artistId);
        window.location.href = 'RequestArtisticUser.html';


        // $.ajax({
        //     url: 'https://localhost:7270/api/admin/disableArtwork/' + id,
        //     method: 'put',
        //     success: function () {
        //         location.reload();
        //     }
        // })

    } )

})
