document.addEventListener("DOMContentLoaded", function () {
    const mainContainer = document.getElementById("fullpage");
    const sections = document.querySelectorAll(".section");
    const navLinks = document.querySelectorAll(".nav-link");
    const dots = document.querySelectorAll(".dot");
    
    let currentIndex = 0;
    let isScrolling = false;
    const totalSections = sections.length;

    // Function to navigate to a specific section index
    function goToSection(index) {
        if (index < 0 || index >= totalSections) return;
        
        currentIndex = index;
        const translateY = -currentIndex * 100;
        mainContainer.style.transform = `translateY(${translateY}vh)`;

        // Update Nav Links Active States
        navLinks.forEach((link) => link.classList.remove("active"));
        const activeLink = document.querySelector(`.nav-link[data-target="${currentIndex}"]`);
        if (activeLink) activeLink.classList.add("active");

        // Update Pagination Dots Active States
        dots.forEach((dot) => dot.classList.remove("active"));
        const activeDot = document.querySelector(`.dot[data-target="${currentIndex}"]`);
        if (activeDot) activeDot.classList.add("active");
    }

    // Handle Mouse Wheel Event with Throttling for Smooth Transition
    window.addEventListener("wheel", function (e) {
        if (isScrolling) return;

        isScrolling = true;
        if (e.deltaY > 0) {
            // Scroll Down
            if (currentIndex < totalSections - 1) {
                goToSection(currentIndex + 1);
            }
        } else {
            // Scroll Up
            if (currentIndex > 0) {
                goToSection(currentIndex - 1);
            }
        }

        // Lock duration timeout to prevent fast jitter scrolling
        setTimeout(() => {
            isScrolling = false;
        }, 900);
    }, { passive: true });

    // Handle Header Navigation Clicks
    navLinks.forEach((link) => {
        link.addEventListener("click", function (e) {
            e.preventDefault();
            const targetIndex = parseInt(this.getAttribute("data-target"));
            goToSection(targetIndex);
        });
    });

    // Handle Pagination Dot Clicks
    dots.forEach((dot) => {
        dot.addEventListener("click", function () {
            const targetIndex = parseInt(this.getAttribute("data-target"));
            goToSection(targetIndex);
        });
    });

    // Handle Keyboard Arrow Navigation
    window.addEventListener("keydown", function (e) {
        if (e.key === "ArrowDown" || e.key === "PageDown") {
            e.preventDefault();
            if (currentIndex < totalSections - 1) goToSection(currentIndex + 1);
        } else if (e.key === "ArrowUp" || e.key === "PageUp") {
            e.preventDefault();
            if (currentIndex > 0) goToSection(currentIndex - 1);
        }
    });

    // Touch Support for Mobile/Trackpad Swiping
    let touchStartY = 0;
    window.addEventListener("touchstart", function (e) {
        touchStartY = e.touches[0].clientY;
    }, { passive: true });

    window.addEventListener("touchend", function (e) {
        let touchEndY = e.changedTouches[0].clientY;
        let diff = touchStartY - touchEndY;

        if (Math.abs(diff) > 50) {
            if (diff > 0 && currentIndex < totalSections - 1) {
                goToSection(currentIndex + 1);
            } else if (diff < 0 && currentIndex > 0) {
                goToSection(currentIndex - 1);
            }
        }
    }, { passive: true });
});