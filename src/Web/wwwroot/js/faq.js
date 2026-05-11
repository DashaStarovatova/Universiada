// /js/faq.js

(function() {
    
    function initAccordion() {
        const allDetails = document.querySelectorAll('.faq-content details');
        
        allDetails.forEach((currentDetails) => {
            currentDetails.addEventListener('toggle', function() {
                if (currentDetails.open) {
                    allDetails.forEach((otherDetails) => {
                        if (otherDetails !== currentDetails && otherDetails.open) {
                            otherDetails.open = false;
                        }
                    });
                }
            });
        });
    }
    
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initAccordion);
    } else {
        initAccordion();
    }
})();