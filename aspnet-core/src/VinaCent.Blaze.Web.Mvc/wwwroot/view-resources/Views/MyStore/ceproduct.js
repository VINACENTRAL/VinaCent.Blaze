const l = abp.localization.getSource('Blaze')
const form = document.getElementById('create-product-form');

window.addEventListener('load', () => {
    tinymce.init({
        selector: '.editor',
        menubar: '',
        init_instance_callback: (editor) => {
            editor.targetElm.classList.add('flyaway');
            const aTag = editor.editorContainer.childNodes[1].querySelector('.tox-statusbar__branding a');
            aTag.href = "https://vinacent.com";
            aTag.innerHTML = '';//`<img src="/vinacent/brand/light.png" style="height: 15px;"/>`;
        }
    });

    initPublishScheduleWatch();
    initEndSellAtVisible();
    initDropzone();
    const featureImage = initFeatureImageUpload();
    console.log(featureImage);
    setTimeout(()=> {
        console.log(featureImage.getFile()?.file)
    }, 10000)

    const tagsInput = initTags();
    form.addEventListener('submit', (e) => {
        if (!$(form).valid()) {
            e.preventDefault();
            return;
        }
        form.querySelectorAll('[name="TagTitles[]"]').forEach((item) => item.remove());
        [...tagsInput.getValue(true)].forEach((item, index) => {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'TagTitles[]';
            input.value = item;
            form.appendChild(input);
        });
    });
})


function initDropzone() {
    const dropDiv = document.querySelector('[data-dropzone]');
    if (!dropDiv) return null;

    if (dropDiv.id?.length === 0) {
        dropDiv.id = `ref-zone-${new Date().getTime()}`;
    }
    const dropPreviewContainer = dropDiv.querySelector('.drop-preview');
    if (!dropPreviewContainer) return null;
    if (dropPreviewContainer.id?.length === 0) {
        dropPreviewContainer.id = `ref-preview-${new Date().getTime()}`;
    }
    const dropzonePreviewNode = dropPreviewContainer.querySelector("li");
    if (!dropzonePreviewNode) return null;
    dropzonePreviewNode.id = "";

    if (dropzonePreviewNode) {
        const previewTemplate = dropPreviewContainer.innerHTML;
        dropPreviewContainer.removeChild(dropzonePreviewNode);

        const dropOptions = {
            url: 'https://httpbin.org/post',
            method: "post",
            previewTemplate: previewTemplate,
            previewsContainer: `#${dropPreviewContainer.id}`,
            autoProcessQueue: false // Prevent auto upload
        };

        if (dropDiv.hasAttribute('data-max-files')) {
            dropOptions['maxFiles'] = dropDiv.dataset.maxFiles;
            dropOptions['init'] = function () {
                this.on('maxfilesexceeded', function (file) {
                    this.removeFile(file);
                });
            }
        }


        return new Dropzone(`#${dropDiv.id}`, dropOptions);
    }

    return null;
}

function initPublishScheduleWatch() {
    const item = document.querySelector('[data-watch]');
    const watchFor = document.querySelector(item.dataset.watch);
    const visibleWhen = item.dataset.when;

    const startSellAt = item.querySelector('[name="StartSellAt"]');
    const endSellAt = item.querySelector('[name="EndSellAt"]');

    startSellAt.disabled = watchFor.value !== visibleWhen;
    endSellAt.disabled = watchFor.value !== visibleWhen;
    if (watchFor.value !== visibleWhen) {
        item.classList.add('d-none');
    } else {
        item.classList.remove('d-none');
    }

    watchFor.addEventListener("change", () => {
        startSellAt.disabled = watchFor.value !== visibleWhen;
        endSellAt.disabled = watchFor.value !== visibleWhen;
        if (watchFor.value !== visibleWhen) {
            item.classList.add('d-none');
        } else {
            item.classList.remove('d-none');
        }
    });
}

function initEndSellAtVisible() {
    const input = document.querySelector('[data-disable-follow]');
    const switchInput = document.getElementById(input.dataset.disableFollow);

    input.disabled = !switchInput.checked;
    if (!switchInput.checked) {
        input.classList.add('d-none');
    } else {
        input.classList.remove('d-none');
    }

    switchInput.addEventListener("change", () => {
        input.disabled = !switchInput.checked;
        if (!switchInput.checked) {
            input.classList.add('d-none');
        } else {
            input.classList.remove('d-none');
        }
    });
}

function initTags() {
    const tagsInputElement = document.getElementById('product-tags-input');
    const tagsInput = new Choices(tagsInputElement, {
        removeItemButton: true
    });

    return tagsInput;
}

function initFeatureImageUpload() {
    // FilePond
    FilePond.registerPlugin(
        FilePondPluginImagePreview
    );

    const options = {
        labelIdle: l(LKConstants.DropFilesHereOrClickToUpload),
        imagePreviewHeight: 400,
        storeAsFile: true,
        onaddfile: (e, f) => {
            console.log(e, f)
        }
    };

    return FilePond.create(document.querySelector('.product-feature-image'), options);
}