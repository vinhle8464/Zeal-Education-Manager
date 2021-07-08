$(document).ready(function () {
   
    //---------------- batch ---------------------------
    // get data to modal edit Batch
    $('table .editbatch').on('click', function () {

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
    // get data to modal edit Batch
    $('table .editaccount').on('click', function () {

        var accountId = $(this).parent().find("#accountId").val();

        $.ajax({
            type: 'GET',
            data: { accountId: accountId},
            url: ' /accounts/findajax',
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
                var dob = new Date(result.dob);
               
                var Dob = dob.getFullYear() + "/" + (dob.getMonth() + 1) + "/" + dob.getDate();
               
                $('#ModalEdit #roleaccount').val(result.roleId);
                $('#ModalEdit #classaccount').val(result.classId);
                $('#ModalEdit #usernameaccount').val(result.username);
                $('#ModalEdit #fullnameaccount').val(result.fullname);
                $('#ModalEdit #emailaccount').val(result.email);
                $('#ModalEdit #dobaccount').val(Dob);
                $('#ModalEdit #addressaccount').val(result.address);
                if (result.gender === true) {
                    $('#ModalEdit #maleaccount').prop('checked', true);
                } else {
                    $('#ModalEdit #femaleaccount').prop('checked', true);
                }
                $('#ModalEdit #phoneaccount').val(result.phone);
                if (result.active === true) {
                    $('#ModalEdit #activeaccount').val("Actived");
                } else {
                    $('#ModalEdit #activeaccount').val("Inactive");

                }
                $('#ModalEdit #avataraccount').attr('src', '../../images/' + result.avatar);
            }
        });
    });

    $("#listClassName").autocomplete({
        source: "/accounts/listClass",
    });
    $("#listClassName").autocomplete("option", "appendTo", ".eventInsForm");

    $("#listScholarship").autocomplete({
        source: "/accounts/listScholarship",
    });
    $("#listScholarship").autocomplete("option", "appendTo", ".eventInsForm");

    //---------------- account ---------------------------

    //---------------- role ---------------------------
    $('table .editrole').on('click', function () {

        var idrole = $(this).parent().find("#idrole").val();

        $.ajax({
            type: 'GET',
            data: { idrole: idrole},
            url: ' /admin/roles/findajax',
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
                $('#ModalEdit #roleid').val(result.roleId);
                $('#ModalEdit #namerole').val(result.roleName);
                $('#ModalEdit #rolename').val(result.roleName);
                $('#ModalEdit #descrole').val(result.desc);
            }
        });
    });
    //---------------- role ---------------------------

    //---------------- Scholarship ---------------------------
    $('table .editscholarship').on('click', function () {

        var idscholarship = $(this).parent().find("#idscholarship").val();

        $.ajax({
            type: 'GET',
            data: { idscholarship: idscholarship },
            url: ' /admin/scholarships/findajax',
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
                $('#ModalEdit #idscholarship').val(result.scholarshipId);
                $('#ModalEdit #namescholarship').val(result.scholarshipName);
                $('#ModalEdit #discountscholarship').val(result.discount);
                $('#ModalEdit #descscholarship').val(result.desc);
                $('#ModalEdit #statusscholarship').val(result.status);

            }
        });
    });
    //---------------- Scholarship ---------------------------

    //---------------- Enquiry ---------------------------
    $('table .editenquiry').on('click', function () {

        var enquiryid = $(this).parent().find("#enquiryid").val();

        $.ajax({
            type: 'GET',
            data: { enquiryid: enquiryid },
            url: ' /admin/enquiries/findajax',
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
                $('#ModalEdit #idenquiry').val(result.id);
                $('#ModalEdit #titleenquiry').val(result.title);
                $('#ModalEdit #answerenquiry').val(result.answer);
                $('#ModalEdit #statusenquiry').val(result.status);

            }
        });
    });
    //---------------- Enquiry ---------------------------


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
//search autocomplete
$("#searchProfessional").autocomplete({
    source: "/admin/professionals/searchautocomplete",
});

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



//---------------- Class ---------------------------


$('table .editClass').on('click', function () {

    var classid = $(this).parent().find("#classid").val();

    $.ajax({
        type: 'GET',
        data: { classid: classid },
        url: ' /admin/classes/findajax',
        dataType: 'json',
        contentType: 'application/json',
        success: function (result) {

            $('#ModalEdit #idclass').val(result.classId);
            $('#ModalEdit #nameclass').val(result.className);
            $('#ModalEdit #numberclass').val(result.numberOfStudent);
            $('#ModalEdit #descclass').val(result.desc);
            $('#ModalEdit #statusclass').val(result.status);
            $('#ModalEdit #nameclasss').val(result.className);

        }
    });
});
//---------------- Class ---------------------------


//---------------- Exam ---------------------------


$('table .editExam').on('click', function () {

    var examid = $(this).parent().find("#examid").val();

    $.ajax({
        type: 'GET',
        data: { examid: examid },
        url: ' /admin/exams/findajax',
        dataType: 'json',
        contentType: 'application/json',
        success: function (result) {

            $('#ModalEdit #idexam').val(result.examId);
            $('#ModalEdit #idsubject').val(result.subjectId);
            $('#ModalEdit #statusexam').val(result.status);
            $('#ModalEdit #titleexam').val(result.title);
            $('#ModalEdit #descexam').val(result.desc);
        }
    });
});
//---------------- Exam ---------------------------


//---------------- Course ---------------------------
//search autocomplete
$("#searchCourse").autocomplete({
    source: "/admin/courses/searchautocomplete",
});

$('table .editcourse').on('click', function () {

    var courseid = $(this).parent().find("#courseid").val();

    $.ajax({
        type: 'GET',
        data: { courseid: courseid },
        url: ' /admin/courses/findajax',
        dataType: 'json',
        contentType: 'application/json',
        success: function (result) {
            $('#ModalEdit #courseidid').val(result.courseId);
            $('#ModalEdit #namecourse').val(result.courseName);
            $('#ModalEdit #feecourse').val(result.fee);
            $('#ModalEdit #termcourse').val(result.term);
            $('#ModalEdit #certificatecourse').val(result.certificate);
            $('#ModalEdit #desccourse').val(result.desc);
            $('#ModalEdit #coursestatus').val(result.status);

            
        }
    });
});
//---------------- Course ---------------------------


//---------------- Subject ---------------------------

$('table .editsubject').on('click', function () {

    var subjectid = $(this).parent().find("#subjectid").val();

    $.ajax({
        type: 'GET',
        data: { subjectid: subjectid },
        url: ' /admin/subjects/findajax',
        dataType: 'json',
        contentType: 'application/json',
        success: function (result) {
            $('#ModalEdit #subjectidid').val(result.subjectId);
            $('#ModalEdit #namesubject').val(result.subjectName);
            $('#ModalEdit #namesubject1').val(result.subjectName);
            $('#ModalEdit #descsubject').val(result.desc);
            $('#ModalEdit #subjectsatus').val(result.status);

        }
    });
});
//---------------- Subject ---------------------------


//---------------- Course Subject ---------------------------
$("#courselist").autocomplete({
    source: "/admin/coursesubjects/listCourse",
});
$("#courselist").autocomplete("option", "appendTo", ".eventInsForm");

$(document).ready(function () {
    $('#courselist').on('change', function () {
        $('subjectlist option').remove();
        var courseName = $('#courselist').val();
        $.ajax({
            type: 'GET',
            data: { courseName: courseName },
            url: '/admin/coursesubjects/findSubject',
            dataType: 'json',
            contentType: 'application/json',
            success: function (listSubject) {
                var s = '';
                for (var i = 0; i < listSubject.length; i++) {
                    s += '<option value="' + listSubject[i].id + '">' + listSubject[i].name + '</option>';
                }
                $('#subjectlist').html(s);
            }
        });
    });
});
//----------------  Course Subject ---------------------------


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

//---------------- schedule ---------------------------
// get data to modal edit Batch
$('table .editschedule').on('click', function () {

    var scheduleid = $(this).parent().find("#scheduleid").val();
    var subjectid = $(this).parent().find("#subjectid").val();

    $.ajax({
        type: 'GET',
        data: { scheduleid: scheduleid },
        url: ' /admin/schedule/findajax',
        dataType: 'json',
        contentType: 'application/json',
        success: function (result) {
            $('#ModalEdit #idclass').val(result.classId);
            $('#ModalEdit #statusschedule').val(result.status);
            $('#ModalEdit #idschedule').val(result.scheduleId);
            $('#ModalEdit #subjectidsche').val(result.subjectId);
            $('#ModalEdit #subjectidschedule').val(result.subjectId);

            
        }
    });

    $.ajax({
        type: 'GET',
        data: { subjectid: subjectid },
        url: '/admin/schedule/findFaculty',
        dataType: 'json',
        contentType: 'application/json',
        success: function (listFaculty) {
            var s = '';
            for (var i = 0; i < listFaculty.length; i++) {
                s += '<option value="' + listFaculty[i].id + '">' + listFaculty[i].name + '</option>';
            }
            $('#listFaculty').html(s);
        }
    });
});
//---------------- schedule ---------------------------



//---------------- test schedule ---------------------------
// get data to modal edit Batch
$('table .edittestschedule').on('click', function () {

    var testscheduleid = $(this).parent().find("#testscheduleid").val();
    var examid = $(this).parent().find("#examid").val();

    $.ajax({
        type: 'GET',
        data: { testscheduleid: testscheduleid },
        url: ' /admin/testschedules/findajax',
        dataType: 'json',
        contentType: 'application/json',
        success: function (result) {
            $('#ModalEdit #idtestschedule').val(result.testScheduleId);
            $('#ModalEdit #idclass').val(result.classId);
            $('#ModalEdit #statusschedule').val(result.status);
            $('#ModalEdit #examid').val(result.examId);
            $('#ModalEdit #idexam').val(result.examId);
        }
    });

    $.ajax({
        type: 'GET',
        data: { examid: examid },
        url: '/admin/testschedules/findFaculty',
        dataType: 'json',
        contentType: 'application/json',
        success: function (listFaculty) {
            var s = '';
            for (var i = 0; i < listFaculty.length; i++) {
                s += '<option value="' + listFaculty[i].id + '">' + listFaculty[i].name + '</option>';
            }
            $('#facultyid').html(s);
        }
    });
});
//---------------- test schedule ---------------------------

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
    if (new Date(UserDate).getTime() < ToDate.getTime()) {
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

// 
setTimeout(function () {
    $('#notification').fadeOut('fast');
}, 3000); // <-- time in milliseconds}