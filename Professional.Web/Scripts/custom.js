$(function () {
    $(document).ready(function () {
        // Endorsement box scripts
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
        $(".dropdown-menu li a").click(function () {
            localStorage.setItem("filter", $(this).text());
        });

        if ($(".dropdown.filter").length > 0 && localStorage.getItem("filter") !== null) {
            $(".dropdown-menu li a").parents(".dropdown.filter").find('.btn').append(": " + localStorage.getItem("filter"));
        }

        // Filtered listing by field scripts (AJAX)
        $(".filterLink").on("click", function () {
            var identificator = $(this).parent().parent().attr('data-unique')
            $('#input' + identificator).val($(this).text());
            $('#form' + identificator).submit();
        });

        $(".deleteLink").on("click", function () {
            var identificator = $(this).parent().parent().attr('data-type')
            $('#input' + identificator).val($(this).attr('data-query'));
            $('#form' + identificator).submit();
        });
    });
});


