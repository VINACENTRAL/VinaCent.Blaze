/*
Template Name: Velzon - Admin & Dashboard Template
Author: Themesbrand
Version: 1.6.0
Website: https://Themesbrand.com/
Contact: Themesbrand@gmail.com
File: Common Plugins Js File
*/

//Common plugins
if (document.querySelectorAll("[toast-list]") || document.querySelectorAll('[data-choices]') || document.querySelectorAll("[data-provider]")) {
    // document.writeln("<script type='text/javascript' src='https://cdn.jsdelivr.net/npm/toastify-js'></script>");
    document.writeln("<script type='text/javascript' src='velzon/libs/choices.js/public/assets/scripts/choices.min.js'></script>");
    document.writeln("<script type='text/javascript' src='velzon/libs/flatpickr/flatpickr.min.js'></script>");

    const flatPickerl10ns = [];
    document.querySelectorAll("[data-datetime-locale]").forEach((item)=>{
        const locate = item.dataset.datetimeLocale;
        if (!locate.includes('us')) {
            const stringBuilder = `<script type="text/javascript" src="velzon/libs/flatpickr/l10n/${locate}.js"></script>`;
            if (!flatPickerl10ns.includes(stringBuilder)) {
                flatPickerl10ns.push(stringBuilder);
                document.writeln(stringBuilder);
            }   
        } else {
            item.removeAttribute('data-datetime-locale');
        }
    });
}