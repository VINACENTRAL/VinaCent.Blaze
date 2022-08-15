var app = app || {};
(function () {
    app.htmlUtils = {
        htmlEncodeText: function (value) {
            return $("<div/>").text(value).html();
        },

        htmlDecodeText: function (value) {
            return $("<div/>").html(value).text();
        },

        htmlEncodeJson: function (jsonObject) {
            return JSON.parse(app.htmlUtils.htmlEncodeText(JSON.stringify(jsonObject)));
        },

        htmlDecodeJson: function (jsonObject) {
            return JSON.parse(app.htmlUtils.htmlDecodeText(JSON.stringify(jsonObject)));
        }
    };

    // =============================================================== //
    $.fn.slug = function () {
        const currentInputSlugField = $(this);
        const inputRef = currentInputSlugField.attr('slug-of');

        if (!inputRef || inputRef?.length === 0) return;

        const input = document.querySelector(`input${inputRef}`);
        input.addEventListener('input', () => {
            currentInputSlugField.val(renderSlug(input.value));
        });
    }

    $('[slug-of]')?.slug();
    // =============================================================== //
})();

const renderSlug = function (str) {
    str = str.replace(/^\s+|\s+$/g, ''); // trim
    str = str.toLowerCase();

    // remove accents, swap ñ for n, etc
    const from = 'aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ·/_,:;';
    const to = 'aAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAbBcCdDdDeEeEeEeEeEeEeEeEeEeEeEeEfFgGhHiIiIiIiIiIiIjJkKlLmMnNoOoOoOoOoOoOoOoOoOoOoOoOoOoOoOoOoOoOpPqQrRsStTuUuUuUuUuUuUuUuUuUUuUuUvVwWxXyYyYyYyYyYyYzZ------';
    for (let i = 0, l = from.length; i < l; i++) {
        str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
    }

    str = str.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
        .replace(/\s+/g, '-') // collapse whitespace and replace by -
        .replace(/-+/g, '-'); // collapse dashes

    return str;
};