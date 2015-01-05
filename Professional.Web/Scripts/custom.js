﻿$(function () {
    $(document).ready(function () {
        // Load more chats
        $(".dropdown-menu.position-dropdown").on("click", "a.load-more", function (event) {
            event.stopPropagation();
            $('#chat-list-form').submit();
        });
        // Load more chats

        // Set max date for datepicker
        $(function () {
            $('[type="date"].datecontrol').prop('max', function () {
                return new Date().toJSON().split('T')[0];
            });
        });
        // Set max date for datepicker

        // Endorsement box
        $("#dialog").dialog({
            autoOpen: false,
            title: "Endorse User",
            width: 380,
            height: 300,
            modal: true,
            buttons: {
                "Save": function () {
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
        // Endorsement box

        // Admin pop-up
        $('.edit-link').click(function () {
            $('#ajax-item-id').attr('value', $(this).attr('data-id'));
            $('#admin-item').removeClass('hidden-item');
            $("#admin-item").dialog("open");
            $('#ajax-get-item').submit();
        });

        $("#admin-item").dialog({
            autoOpen: false,
            title: "Edit Item",
            width: 350,
            height: 350,
            modal: true,
            buttons: {
                "Save": function () {
                    $("#edit-admin").submit();
                },
                "Cancel": function () { $(this).dialog('close'); }
            }
        });

        $('.create-link').click(function () {
            $('#create-item').removeClass('hidden-item');
            $("#create-item").dialog("open");
        });

        $("#create-item").dialog({
            autoOpen: false,
            title: "Create Item",
            width: 350,
            height: 350,
            modal: true,
            buttons: {
                "Save": function () {
                    $("#create-admin").submit();
                },
                "Cancel": function () { $(this).dialog('close'); }
            }
        });
        // Admin pop-up

        // Filtered listing by field scripts (AJAX)
        $(".dropdown-menu li a").click(function () {
            localStorage.setItem("filter", $(this).text());
        });

        if ($(".dropdown.filter").length > 0 && localStorage.getItem("filter") !== null) {
            $(".dropdown-menu li a").parents(".dropdown.filter").find('.btn').append(": " + localStorage.getItem("filter"));
        }

        $(".filterLink").on("click", function () {
            var identificator = $(this).parent().parent().attr('data-unique')
            $('#input' + identificator).val($(this).text());
            $('#form' + identificator).submit();
        });
        // Filtered listing by field scripts (AJAX)

        // Delete item on private profile page
        $("div.short-listing").on("click", "a", function () {
            var identificator = $(this).parent().parent().attr('data-type')
            $('#input' + identificator).val($(this).attr('data-query'));
            $('#form' + identificator).submit();
        });

        // Update available
        $('.glif-updates .dropdown-toggle span').each(function () {
            if ($(this).attr('data-messaged') === 'True') {
                $(this).addClass('active-updates');
            }
            else {
                $(this).removeClass('active-updates');
            }
        });
    });
});


