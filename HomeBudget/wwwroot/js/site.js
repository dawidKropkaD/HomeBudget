$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})


//////////////////////////////////////////////////////////////////////////////////////////////
//////Date range
//////////////////////////////////////////////////////////////////////////////////////////////
$(function () {
    TryCheckDateRangeRadioHelper();
})

$('#Start, #End').change(function () {
    $('input[type=radio][name=lastRadio]').prop('checked', false);

    TryCheckDateRangeRadioHelper();
});

$('input[type=radio][name=lastRadio]').change(function () {
    var startDate;

    if (this.id == 'lastWeekCheck') {
        startDate = new Date($('#LastWeekStart').val());
    }
    else if (this.id == 'lastMonthCheck') {
        startDate = new Date($('#LastMonthStart').val());
    }
    else if (this.id == 'lastYearCheck') {
        startDate = new Date($('#LastYearStart').val());
    }

    var currenDate = new Date($('#CurrentDate').val());
    $('#Start').val(GetDateAsString(startDate));
    $('#End').val(GetDateAsString(currenDate));
});

function GetDateAsString(date) {
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);

    return date.getFullYear() + "-" + (month) + "-" + (day);
}

function TryCheckDateRangeRadioHelper() {
    var startDate = new Date($('#Start').val());
    var endDate = new Date($('#End').val());

    if (IsLastWeek(startDate, endDate)) {
        $('#lastWeekCheck').prop('checked', true);
    }
    if (IsLastMonth(startDate, endDate)) {
        $('#lastMonthCheck').prop('checked', true);
    }
    if (IsLastYear(startDate, endDate)) {
        $('#lastYearCheck').prop('checked', true);
    }
}

function IsLastWeek(dateFrom, dateTo) {
    var currentDate = new Date($('#CurrentDate').val());
    var weekAgoDate = new Date($('#LastWeekStart').val());

    if (DatesOnlyAreEquals(dateFrom, weekAgoDate) && DatesOnlyAreEquals(dateTo, currentDate)) {
        return true;
    }

    return false;
}

function IsLastMonth(dateFrom, dateTo) {        
    var currentDate = new Date($('#CurrentDate').val());
    var monthAgoDate = new Date($('#LastMonthStart').val());

    if (DatesOnlyAreEquals(dateFrom, monthAgoDate) && DatesOnlyAreEquals(dateTo, currentDate)) {
        return true;
    }

    return false;
}

function IsLastYear(dateFrom, dateTo) {
    var currentDate = new Date($('#CurrentDate').val());
    var yearAgoDate = new Date($('#LastYearStart').val());

    if (DatesOnlyAreEquals(dateFrom, yearAgoDate) && DatesOnlyAreEquals(dateTo, currentDate)) {
        return true;
    }

    return false;
}

function DatesOnlyAreEquals(date1, date2) {
    date1.setHours(0, 0, 0, 0);
    date2.setHours(0, 0, 0, 0);

    if (date1.getTime() == date2.getTime()) {
        return true;
    }

    return false;
}
//////////////////////////////////////////////////////////////////////////////////////////////
//////END Date range
//////////////////////////////////////////////////////////////////////////////////////////////