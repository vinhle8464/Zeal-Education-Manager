$(document).ready(function () {
    // get data to modal edit
    $('table .edit').on('click', function () {

        var course = $(this).parent().find("#courses").val();
        var classes = $(this).parent().find("#classes").val();

        $.ajax({
            type: 'GET',
            data: { courseid: course, classid: classes },
            url: ' /batches/findajax',
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
                var sdate = new Date(result.startDate);
                var edate = new Date(result.endDate);

                var startdate = sdate.getFullYear() + "/" + (sdate.getMonth() + 1) + "/" + sdate.getDate();
                var enddate = edate.getFullYear() + "/" + (edate.getMonth() + 1) + "/" + edate.getDate();

                $('#ModalEdit #courseidhd').val(result.courseId);
                $('#ModalEdit #classidhd').val(result.classId);
                $('#ModalEdit #courseid').val(result.courseId);
                $('#ModalEdit #classid').val(result.classId);
                if (result.graduate === true) {
                    $('#ModalEdit #graduated').prop('checked', true);
                } else {
                    $('#ModalEdit #undergraduate').prop('checked', true);
                }
                $('#ModalEdit #startdate').val(startdate);
                $('#ModalEdit #enddate').val(enddate);
            }
        });
    });
    //---------------- batch ---------------------------
    //search autocomplete
    $("#searchkeyword").autocomplete({
        source: "/batches/searchautocomplete",
    });

    //listCourse autocomplete
    //$("#listCourse").autocomplete({
    //    source: "/batches/listCourse",
    //});
    //$("#listCourse").autocomplete("option", "appendTo", ".eventInsForm");

    //listClass autocomplete
    $("#listClass").autocomplete({
        source: "/batches/listClass",
    });
    $("#listClass").autocomplete("option", "appendTo", ".eventInsForm");

    ////$(document).ready(function () {
    //$('#listClass').on('change', function () {
    //    $('listCourse option').remove();
    //    var className = $('#listClass').val();
    //    $.ajax({
    //        type: 'GET',
    //        data: { className: className },
    //        url: '/admin/batches/listCourse',
    //        dataType: 'json',
    //        contentType: 'application/json',
    //        success: function (listCourse) {
    //            var s = '<option value="-1">Select a Course</option>';
    //            for (var i = 0; i < listCourse.length; i++) {
    //                s += '<option value="' + listCourse[i].id + '">' + listCourse[i].name + '</option>';
    //            }
    //            $('#listCourse').html(s);
    //        }
    //    });
    //});
    //--------------- batch  ------------------------------


    //---------------- account ---------------------------
    $("#listClassName").autocomplete({
        source: "/accounts/listClass",
    });
    $("#listClassName").autocomplete("option", "appendTo", ".eventInsForm");

    $("#listScholarship").autocomplete({
        source: "/accounts/listScholarship",
    });
    $("#listScholarship").autocomplete("option", "appendTo", ".eventInsForm");

    //---------------- account ---------------------------


    //---------------- class assignment ---------------------------
    $("#cbbClass").autocomplete({
        source: "/classassignments/listClass",
    });
    $("#cbbClass").autocomplete("option", "appendTo", ".eventInsForm");

    //$(document).ready(function () {
    $('#cbbClass').on('change', function () {
        $('cbbSubject option').remove();
        var clasName = $('#cbbClass').val();
        $.ajax({
            type: 'GET',
            data: { className: clasName },
            url: '/admin/classassignments/findSubject',
            dataType: 'json',
            contentType: 'application/json',
            success: function (listSubject) {
                var s = '<option value="-1">Select a Subject</option>';
                for (var i = 0; i < listSubject.length; i++) {
                    s += '<option value="' + listSubject[i].name + '">' + listSubject[i].name + '</option>';
                }
                $('#cbbSubject').html(s);
            }
        });
    });

    $('#cbbSubject').on('change', function () {
        $('cbbFaculty option').remove();
        var subjectName = $('#cbbSubject option:selected').val();
        if (subjectName != -1) {
            $.ajax({
                type: 'GET',
                data: { subjectName: subjectName },
                url: '/admin/classassignments/findFaculty',
                dataType: 'json',
                contentType: 'application/json',
                success: function (listFacluty) {
                    var s = '';
                    for (var i = 0; i < listFacluty.length; i++) {
                        s += '<option value="' + listFacluty[i].id + '">' + listFacluty[i].name + '</option>';
                    }
                    $('#cbbFaculty').html(s);
                }
            });
        }

    });
});
//---------------- class assignment ---------------------------


//---------------- Professional ---------------------------
$("#facultyProfess").autocomplete({
    source: "/admin/professionals/listFaculty",
});
$("#facultyProfess").autocomplete("option", "appendTo", ".eventInsForm");

$(document).ready(function () {
    $('#facultyProfess').on('change', function () {
        $('subjectProfess option').remove();
        var facultyName = $('#facultyProfess').val();
        $.ajax({
            type: 'GET',
            data: { facultyName: facultyName },
            url: '/admin/professionals/findSubject',
            dataType: 'json',
            contentType: 'application/json',
            success: function (listSubject) {
                var s = '';
                for (var i = 0; i < listSubject.length; i++) {
                    s += '<option value="' + listSubject[i].id + '">' + listSubject[i].name + '</option>';
                }
                $('#subjectProfess').html(s);
            }
        });
    });
});
//---------------- Professional ---------------------------


//---------------- FeedBack ---------------------------
$("#feedbackFaculty").autocomplete({
    source: "/feedbacks/listFaculty",
});
$("#feedbackFaculty").autocomplete("option", "appendTo", ".eventInsForm");

$(document).ready(function () {
    $('#feedbackFaculty').on('change', function () {
        $('subjectFeedback option').remove();
        var facultyName = $('#feedbackFaculty').val();
        $.ajax({
            type: 'GET',
            data: { facultyName: facultyName },
            url: '/admin/feedbacks/findSubject',
            dataType: 'json',
            contentType: 'application/json',
            success: function (listSubject) {
                var s = '';
                for (var i = 0; i < listSubject.length; i++) {
                    s += '<option value="' + listSubject[i].id + '">' + listSubject[i].name + '</option>';
                }
                $('#subjectFeedback').html(s);
            }
        });
    });
});
//---------------- FeedBack ---------------------------


$(function () {
    $('#student').hide();
    $('#scholarshipstudent').hide();

    $('#roleid').change(function () {
        $('#student').hide();
        $('#scholarshipstudent').hide();

        var result = $('#roleid').find(":selected").text();
        $('#' + result).show();
        $('#' + 'scholarship' + result).show();

    });
});

$(function () {
    $("#pageSize").change(function () {
        $("#form1").submit();
    });
});

function tempAlert(msg, duration) {
    var el = document.createElement("div");
    el.setAttribute("style", "position:absolute;top:40%;left:20%;background-color:green; font-size: 30px;");
    el.innerHTML = msg;
    setTimeout(function () {
        el.parentNode.removeChild(el);
    }, duration);
    document.body.appendChild(el);
};

// check date smaller then today
function CheckDateSmaller(id) {

    var UserDate = document.getElementById(id).value;
    var ToDate = new Date();
    if (new Date(UserDate).getTime() >= ToDate.getTime()) {
        alert("The Date must be Smaller or Equal to today date");
        $("#" + id).val("MM-dd-yyyy");
        return false;
    }
};

// check date greater then today
function CheckDate(id) {

    var UserDate = document.getElementById(id).value;
    var ToDate = new Date();
    if (new Date(UserDate).getTime() <= ToDate.getTime()) {
        alert("The Date must be Bigger or Equal to today date");
        $("#" + id).val("MM-dd-yyyy");
        return false;
    }
};

function CompareDateStart() {

    var enddt = document.getElementById("enddt").value;
    var startdt = document.getElementById("startdt").value;
    var enddate = document.getElementById("enddate").value;
    var startdate = document.getElementById("startdate").value;

    // start date change after
    if (new Date(startdt).getTime() >= new Date(enddt).getTime()) {
        alert("The Start Date must be Smaller than End Date");
        $("#startdt").val("MM-dd-yyyy");
    }
    if (new Date(startdate).getTime() >= new Date(enddate).getTime()) {
        alert("The Start Date must be Smaller than End Date");
        $("#startdate").val("MM-dd-yyyy");
    }

};

function CompareDateEnd() {

    var enddt = document.getElementById("enddt").value;
    var startdt = document.getElementById("startdt").value;
    var enddate = document.getElementById("enddate").value;
    var startdate = document.getElementById("startdate").value;

    // end date change after
    if (new Date(startdt).getTime() >= new Date(enddt).getTime()) {
        alert("The End Date must be Bigger than Start Date");
        $("#enddt").val("MM-dd-yyyy");
    }
    if (new Date(startdate).getTime() >= new Date(enddate).getTime()) {
        alert("The End Date must be Bigger than Start Date");
        $("#enddate").val("MM-dd-yyyy");
    }
};

function validate(email) {
    var filter = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return filter.test(email);
};
function validateEmail(id) {
    const $result = $("#result");
    const email = $("#" + id).val();
    $result.text("");

    if (validate(email)) {
        return true;
    } else {
        alert(email + " is not valid :(");
        return false;
        // window.location.reload(false);
    }
    return false;
};
