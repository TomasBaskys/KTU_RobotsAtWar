var SelectedBattleId;

function GetBattles() {
    $.get("http://localhost:1235/api/battlefield/getbattles",
        function(battles) {
            FillBattleTable(battles);
        });
}

function RefreshBattlesTable() {
    $.get("http://localhost:1235/api/battlefield/getbattles",
        function(battles) {
            var battleTable = document.getElementById("battles_table");

            while (battleTable.firstChild) {
                battleTable.removeChild(battleTable.firstChild);
            }

            FillBattleTable(battles);
        });
}

function FillBattleTable(battles) {
    var trHtml = '';

    $.each(battles,
        function (i, item) {
            var tableIndex = i + 1;
            trHtml += '<tr class="' + item.BattleId + '" onclick=SetSelectedRoomId("' + item.BattleId + '")><th>' + tableIndex + '</th><td>' + item.BattleName + '</td><td>' + item.HostRobotName + '</td><td>' + 9 + '</td><td>' + 99 + '</td></tr>';
        });

    $(".arena_table_content").append(trHtml);
}

function SetSelectedRoomId(battleId) {

    if (SelectedBattleId != null) {
        var unselectedBattleRow = $('.arena_table_content .' + SelectedBattleId)[0];
        unselectedBattleRow.removeAttribute("style")
    }

    var battleRow = $('.arena_table_content .' + battleId)[0];
    battleRow.style.backgroundColor = "#88F";

    SelectedBattleId = battleId;
}

function JoinBattle() {

    var robotId = sessionStorage.getItem("robotId");
    $.get("http://localhost:1235/api/battlefield/joinbattle?battleId=" + SelectedBattleId + "&robotId=" + robotId)
        .done(function () {
            alert("second success");
        })
        .fail(function () {
            alert("error");
        });
}