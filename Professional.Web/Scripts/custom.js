$(function () {
    $("#dialog").dialog({
        autoOpen: false,
        title: "Endorse User",
        width: 380,
        height: 300,
        modal: true,
        buttons: {
            "Save": function () {
                // Manually submit the form                        
                var form = $('form', this);
                $(form).submit();
            },
            "Cancel": function () { $(this).dialog('close'); }
        }
    });
    $("#endorse-btn").click(function () {
        $("#dialog").dialog("open");
    });
    $("#value-holder").change(function outputUpdate(val) {
        document.querySelector('#value').value = document.querySelector('#value-holder').value;
    });
});