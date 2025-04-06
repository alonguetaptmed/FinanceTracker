// Finance Tracker - JavaScript principal

// Formatter les montants en euros
function formatCurrency(amount) {
    return new Intl.NumberFormat('fr-FR', { style: 'currency', currency: 'EUR' }).format(amount);
}

// Formatter les dates
function formatDate(dateString) {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(dateString).toLocaleDateString('fr-FR', options);
}

// Valider que le montant est positif
function validatePositiveAmount(input) {
    const value = parseFloat(input.value);
    if (isNaN(value) || value <= 0) {
        input.classList.add('is-invalid');
        return false;
    } else {
        input.classList.remove('is-invalid');
        return true;
    }
}

// Initialiser les éléments de l'interface
document.addEventListener('DOMContentLoaded', function () {
    // Activer les tooltips Bootstrap
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });
    
    // Formater automatiquement les champs de montant
    document.querySelectorAll('input[type="number"][step="0.01"]').forEach(function(input) {
        input.addEventListener('blur', function() {
            if (this.value) {
                const value = parseFloat(this.value);
                if (!isNaN(value)) {
                    this.value = value.toFixed(2);
                }
            }
        });
    });
    
    // Confirmation pour les actions de suppression
    document.querySelectorAll('.confirm-delete').forEach(function(button) {
        button.addEventListener('click', function(e) {
            if (!confirm('Êtes-vous sûr de vouloir supprimer cet élément ? Cette action est irréversible.')) {
                e.preventDefault();
            }
        });
    });
    
    // Activer le sélecteur de couleur pour les catégories si présent
    const colorPicker = document.getElementById('Color');
    if (colorPicker) {
        colorPicker.addEventListener('input', function() {
            const colorHex = document.getElementById('colorHex');
            if (colorHex) {
                colorHex.value = this.value;
            }
        });
    }
});
