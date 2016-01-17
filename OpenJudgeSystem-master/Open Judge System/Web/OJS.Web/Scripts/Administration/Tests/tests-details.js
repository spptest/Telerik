﻿function removeOutputAjaxLink() {
    $('#ajax-output-link').hide();
}

function removeInputAjaxLink() {
    $('#ajax-input-link').hide();
}

function testResult(test) {
    var result = '';

    switch (test) {
        case 0: result += '<span class="glyphicon glyphicon-ok text-success" title="Correct answer"></span>'; break;
        case 1: result += '<span class="glyphicon glyphicon-remove text-danger" title="Wrong answer"></span>'; break;
        case 2: result += '<span class="glyphicon glyphicon-time text-danger" title="Time limit"></span>'; break;
        case 3: result += '<span class="glyphicon glyphicon-hdd text-danger" title="Memory limit"></span>'; break;
        case 4: result += '<span class="glyphicon glyphicon-asterisk text-danger" title="Run-time error"></span>'; break;
    }

    return result;
};

function initilizeTestRuns(response) {
    $('#test-runs-button').hide();

    $('#test-runs-grid').kendoGrid({
        dataSource: new kendo.data.DataSource({
            data: response.responseJSON,
            pageSize: 25,
        }),
        pageable: true,
        scrollable: false,
        columns: [
            { field: "Id", title: "Номер" },
            { field: "TimeUsed", title: "Време" },
            { field: "MemoryUsed", title: "Памет" },
            { title: "Резултат", template: '<div> #= testResult(ExecutionResult) # </div>' },
            { field: "CheckerComment", title: "Чекер" },
            { field: "ExecutionComment", title: "Екзекютор" },
            { title: "Решение", template: '<a href="/Contests/Submissions/View/#= SubmissionId #" target="_blank">№#= SubmissionId #</a>' }
        ],
    });
}