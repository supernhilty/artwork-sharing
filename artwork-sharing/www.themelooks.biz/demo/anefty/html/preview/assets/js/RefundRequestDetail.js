// Define the URL parameters to extract the id
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const id = urlParams.get('id');

// If id is not available, you can handle it according to your requirements, for example, redirecting the user to a 404 page
if (!id) {
    window.location.href = '404.html'; // Redirect to 404 page
}

// Define the URL of the API endpoint
const apiUrl = `https://localhost:7270/RefundRequest/${id}`;

// Function to render data on the HTML template
function renderDataOnHTML(data) {
  document.getElementById('product-title').textContent = data[0].id;
  //document.getElementById('available').textContent = `Available ${data.id}`;
  //document.getElementById('love-count').textContent = data.id;
  //document.getElementById('paragraph').textContent = data.paragraph;
  document.getElementById('price').innerHTML = `<h6>Desciption</h6><h3>${data[0].description}</h3>`;
  document.getElementById('reason').innerHTML = `<h6>Reason</h6><h3>${data[0].reason}</h3>`;
  document.getElementById('Time-Rf').innerHTML = `<h6 class="mb-0">${data[0].refundRequestDate}</h6>`;
  document.getElementById('Price-Rf').innerHTML = `<h6 class="mb-0">${data[0].transaction.totalBill}</h6>`;
  
//   document.getElementById('creator-avatar').src = data.creator.avatar;
//   document.getElementById('media-body-creator').getElementsByTagName('h5')[0].textContent = data.creator.name;
//   document.getElementById('owner-avatar').src = data.owner.avatar;
//   document.getElementById('media-body-owner').getElementsByTagName('h5')[0].textContent = data.owner.name;
  
}

// Fetch data from the API
fetch(apiUrl)
  .then(response => {
    // Check if the request was successful
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    // Parse the JSON response
    return response.json();
  })
  .then(data => {
    // Process the retrieved data
    console.log('Data retrieved from the API:', data);
    // Render data on the HTML template
    renderDataOnHTML(data);
  })
  .catch(error => {
    // Handle any errors that occurred during the fetch operation
    console.error('Error fetching data:', error);
  });


  // Khi nhấp vào nút "Deny"
$('#btn-border').click(function() {
    updateRefundRequestStatus('deny');
   
});

// Khi nhấp vào nút "Accept"
$('#btn-sm').click(function() {
    updateRefundRequestStatus('accept');
   
});

function updateRefundRequestStatus(status) {
    // Lấy id của refund request từ URL
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get('id');

    // Kiểm tra xem id có tồn tại không
    if (!id) {
        console.error('Refund request id not found');
        return;
    }

    // Gọi API để cập nhật trạng thái của refund request
    const apiUrl = `https://localhost:7270/RefundRequest/${id}/status?status=${status}`;
    $.ajax({
        url: apiUrl,
        method: 'PUT',
        success: function(response) {
            console.log(`Refund request ${id} status updated to ${status}`);
            // Thực hiện các hành động phản hồi sau khi cập nhật thành công
            window.location.href = 'RefundRequestHome.html';
        },
        error: function(xhr, status, error) {
            console.error('Error updating refund request status:', error);
            // Xử lý lỗi nếu cần thiết
        }
    });
}
