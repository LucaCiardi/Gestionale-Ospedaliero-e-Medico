// Add this to site.js
$(document).ready(function () {
    // Auto-hide alerts after 5 seconds
    setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000);

    // Enable tooltips everywhere
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });
});
