window.addEventListener('load', () => {
    tinymce.init({
        selector: '.editor',
        menubar: '',
        init_instance_callback: (editor) => {
            const aTag = editor.editorContainer.childNodes[1].querySelector('.tox-statusbar__branding a');
            aTag.href = "https://vinacent.com";
            aTag.innerHTML = '';//`<img src="/vinacent/brand/light.png" style="height: 15px;"/>`;
        }
    });
})