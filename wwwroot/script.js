document.addEventListener('DOMContentLoaded', function () {
    fetch('/api/invoice')
        .then(resp => {
            if (!resp.ok) {
                throw new Error('Network response was not ok');
            }
            return resp.json();
        })
        .then(data => {
            let html = `<h2>Invoice #${data.invoiceId} - ${data.customerName}</h2>`;
            html += '<ul>';
            data.items.forEach(item => {
                html += `<li>${item.name} - $${item.price.toFixed(2)}</li>`;
            });
            html += '</ul>';
            html += `<p><strong>Total: $${data.total.toFixed(2)}</strong></p>`;
            document.getElementById('invoice-container').innerHTML = html;
        })
        .catch(err => {
            console.error('Failed to load invoice:', err);
            document.getElementById('invoice-container').innerHTML =
                '<p>Failed to load invoice.</p>';
        });
});
