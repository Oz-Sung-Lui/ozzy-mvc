var coll = document.getElementsByClassName("collapsible");
var i;
for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function() {
        this.classList.toggle("active");
        var content = this.parentNode.parentNode.parentNode.parentNode.nextElementSibling;
        var tfoot = this.parentNode.parentNode.parentNode;
        if (content.style.getPropertyValue('--coll') === "false") {
            tfoot.style.setProperty('border-radius', '0 0 2.5vmin 2.5vmin')
            content.style.setProperty('--coll', "true");
            content.style.display = "none";
        } else {
            tfoot.style.setProperty('border-radius', '0 0 2.5vmin 0')
            content.style.setProperty('--coll', "false");
            content.style.display = "block";
        }
    });
}