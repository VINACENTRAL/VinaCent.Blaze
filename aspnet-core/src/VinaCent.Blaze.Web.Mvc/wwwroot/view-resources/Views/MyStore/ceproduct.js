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
    
    const dropzone = initDropzone();
    const featureImage = initFeatureImageUpload();
    const tagsInput = initTags();
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        if (!$(form).valid()) {
            return;
        }
        
        if (!featureImage.getFile()?.file) {
            abp.message.warn(l(LKConstants.PleaseAddProductFeatureImage));
            return;
        }

        abp.ui.setBusy($(form));
        
        // Process to  render tags
        form.querySelectorAll('[name="TagTitles[]"]').forEach((item) => item.remove());
        [...tagsInput.getValue(true)].forEach((item, index) => {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'TagTitles[]';
            input.value = item;
            form.appendChild(input);
        });

        // Process form data
        const formData = new FormData(form);

        // Add feature image to formData
        formData.append('FeatureImageFile', featureImage.getFile()?.file);

        // Add product images
        const productImages = dropzone?.getAcceptedFiles();
        if (productImages?.length > 0) {
            [...productImages].forEach((item)=> {
                formData.append('Images', item);
            });
        }

        abp.ajax({
            url: abp.appPath + 'api/services/app/ShopProduct/Create',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
        }).done(function () {
            abp.notify.info(l(LKConstants.SavedSuccessfully));
        }).always(function () {
            abp.ui.clearBusy($(form));
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
    };

    return FilePond.create(document.querySelector('.product-feature-image'), options);
}