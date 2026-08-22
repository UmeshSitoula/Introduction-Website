document.addEventListener("DOMContentLoaded", () => {
    const sections = document.querySelectorAll(".section");
    const navLinks = document.querySelectorAll(".nav-link");
    const dots = document.querySelectorAll(".dot");

    // Function to update active nav items and dots based on scroll position
    const changeLinkState = () => {
        let index = sections.length;

        while(--index >= 0 && window.scrollY + 200 < sections[index].offsetTop) {}

        navLinks.forEach((link) => link.classList.remove("active"));
        dots.forEach((dot) => dot.classList.remove("active"));

        if(index >= 0) {
            if(navLinks[index]) navLinks[index].classList.add("active");
            if(dots[index]) dots[index].classList.add("active");
        }
    };

    window.addEventListener("scroll", changeLinkState);

    // Click behavior for side pagination dots
    dots.forEach((dot, idx) => {
        dot.addEventListener("click", () => {
            sections[idx].scrollIntoView({ behavior: "smooth" });
        });
    });

    // Click behavior for header navigation links
    navLinks.forEach((link, idx) => {
        link.addEventListener("click", (e) => {
            e.preventDefault();
            sections[idx].scrollIntoView({ behavior: "smooth" });
        });
    });
});