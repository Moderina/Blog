// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const elements = document.getElementsByClassName("text");

// Iterate over each element
for (let i = 0; i < elements.length; i++) {
    const element = elements[i];

    // Check if the element's text contains "#"
    if (element.textContent.includes("#")) {
        // Replace "#" with colored spans
        element.innerHTML = element.innerHTML.replace(/#(\w+)/g, '<span style="color: #58a4f1;">#$1</span>');
    }
}