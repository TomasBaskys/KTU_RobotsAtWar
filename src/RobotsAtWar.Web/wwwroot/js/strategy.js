var tableRowTemplate =
        '<tr>' +
            '<th scope="row">{rowNumber}</th>' +
            '<td>{action}</td>' +
            '<td>{strength}</td>' +
        '</tr>';

function onPageLoad() {
    getStrategy();
}

function getStrategy() {

    var robotId = sessionStorage.getItem("robotId");

    $.get('http://PCTOMBASL1:1235/api/strategy/get?' +
        'robotId=' + robotId,
        function (response) {
            fillStrategyTable(response.Strategy);
        });
}

function fillStrategyTable(strategy) {

    var tableBody = $(".strategy_table tbody");

    for (var i = 0; i < strategy.length; i++) {

        var tableRow = tableRowTemplate;

        tableRow = tableRow.replace("{rowNumber}", i + 1);
        tableRow = tableRow.replace("{action}", strategy[i].Action);
        tableRow = tableRow.replace("{strength}", strategy[i].Level);

        tableBody.append(tableRow);
    }
}

function addStrategy(action, level) {

    updateRobotStrategy(action, level);

    var tableBody = $(".strategy_table tbody");
    var tableRow = tableRowTemplate;

    tableRow = tableRow.replace("{rowNumber}", tableBody.children().length+1);
    tableRow = tableRow.replace("{action}", action);
    tableRow = tableRow.replace("{strength}", level);

    tableBody.append(tableRow);
}

function updateRobotStrategy(action, level) {

    var robotId = sessionStorage.getItem("robotId");

    $.get('http://PCTOMBASL1:1235/api/strategy/update?' +
        'robotId=' + robotId +
        '&action=' + action +
        '&level=' + level,
        function (response) {
            fillStrategyTable(response.Strategy);
        });
}

function removeLastItem() {

    var robotId = sessionStorage.getItem("robotId");

    $.get('http://PCTOMBASL1:1235/api/strategy/remove?' +
        'robotId=' + robotId);

    var table = document.getElementsByClassName("strategy_table")[0];
    var rowCount = table.rows.length;

    if (rowCount > 1) {
        table.deleteRow(rowCount - 1);
    }
}