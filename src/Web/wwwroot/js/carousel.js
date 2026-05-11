document.addEventListener('DOMContentLoaded', function() {
    const container = document.getElementById('challenge-carousel');
    
    if (!container) return;
    
    const nextBtn = container.querySelector('.nav-button.next');
    const items = container.querySelectorAll('.content-item');
    const indicators = container.querySelectorAll('.indicator');
    
    let currentIndex = 0;
    const totalItems = items.length;
    
    function updateCarousel(index) {
        items.forEach(item => item.classList.remove('active'));
        items[index].classList.add('active');
        
        indicators.forEach((ind, i) => {
            if (i === index) {
                ind.classList.add('active');
            } else {
                ind.classList.remove('active');
            }
        });
    }
    
    function next() {
        currentIndex = (currentIndex + 1) % totalItems;
        updateCarousel(currentIndex);
    }
    
    if (nextBtn) {
        nextBtn.addEventListener('click', next);
    }
});