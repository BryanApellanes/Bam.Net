
function effectViewModel(el) {
    $(el).off("change").on("change", function () {
        var ef = $(this).val();
        bam.app("sample").setHelloEffect(ef, true);
        bam.app("sample").setGoodByeEffect(ef, true);
    });
}