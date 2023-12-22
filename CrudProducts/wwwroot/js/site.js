// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
// site.js

// Fonction pour mettre à jour le nombre d'articles dans le panier
function updateCartCount() {
    const cartCountElement = document.getElementById('cartCount');
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    const totalCount = cart.reduce((sum, item) => sum + item.quantity, 0);
    cartCountElement.innerText = totalCount;
}
