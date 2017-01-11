var SelectedBattleId;
var SelectedBattleName;
var SelectedBattleHostId;

function onPageLoad() {

    GetBattles();

    $('#demoBattleButton').on('click', function () {
        startDemoBattle();
    });
}

function GetBattles() {
    $.get('http://PCTOMBASL1:1235/api/battlefield/getbattles',
        function (battles) {
            FillBattleTable(battles);
        });
}

function RefreshBattlesTable() {
    $.get('http://PCTOMBASL1:1235/api/battlefield/getbattles',
        function (battles) {
            var battleTable = document.getElementById('battles_table');

            while (battleTable.firstChild) {
                battleTable.removeChild(battleTable.firstChild);
            }

            FillBattleTable(battles);
        });
}

function hostBattle() {
    var robotId = sessionStorage.getItem('robotId');
    var battleName = $('#create_battle_modal #battle-name')[0].value;
    var battleType = $('#create_battle_modal #public_room')[0].checked
        ? 'public'
        : 'private';

    $.get('http://PCTOMBASL1:1235/api/battlefield/hostbattle?' +
            'robotId=' + robotId +
            '&battleName=' + battleName +
            '&roomType=' + battleType,
            function (response) {
                var battleId = response;
                RefreshBattlesTable();

                $('#join_battle_modal #join_button')[0].className =
                    $('#join_battle_modal #join_button')[0].className.replace("active", "disabled");

                fillJoinModal(battleName, battleId, robotId);
                $('#create_battle_modal').modal('hide');
                $('#join_battle_modal').modal('show');
            });
}

function startDemoBattle() {
    var robotId = sessionStorage.getItem('robotId');

    $.get('http://PCTOMBASL1:1235/api/battlefield/startdemobattle?robotId=' + robotId,
        function (battleId) {
            sessionStorage.setItem('battleFieldId', battleId);
        })
        .done(function () {
            window.location.href = '/RobotsAtWar/battle.html';

        })
        .fail(function () {
            alert('An error occurred.');
        });
}

function FillBattleTable(battles) {
    var trHtml = '';

    $.each(battles,
        function (i, item) {
            var tableIndex = i + 1;
            trHtml += '<tr class="' + item.BattleId + '" onclick=SetSelectedRoomId("' + item.BattleId + '")><th>' + tableIndex + '</th><td>' + item.BattleName + '</td><td>' + item.HostRobotName + '</td><td>' + 9 + '</td><td>' + 99 + '</td></tr>';
        });

    $('.arena_table_content').append(trHtml);
}

function SetSelectedRoomId(battleId) {

    if (SelectedBattleId != null) {
        var unselectedBattleRow = $('.arena_table_content .' + SelectedBattleId)[0];
        unselectedBattleRow.removeAttribute('style');
    }

    var battleRow = $('.arena_table_content .' + battleId)[0];
    battleRow.style.backgroundColor = '#88F';

    SelectedBattleId = battleId;
}

function JoinBattle() {

    var robotId = sessionStorage.getItem('robotId');
    $.get('http://PCTOMBASL1:1235/api/battlefield/joinbattle?' +
            'battleId=' + SelectedBattleId +
            '&robotId=' + robotId +
            '&playType=' + 'auto'
            )
        .done(function () {
            alert('second success');
        })
        .fail(function () {
            alert('error');
        });
}

function joinModal() {
    var battleId = SelectedBattleId;
    var robotId = sessionStorage.getItem('robotId');

    var battleName;
    var opponentId;

    $.get('http://PCTOMBASL1:1235/api/battlefield/getbattle?' +
            'battleFieldId=' + SelectedBattleId,
            function (response) {
                battleName = response.BattleName;
                opponentId = response.HostRobotId;

                fillJoinModal(battleName, battleId, robotId, opponentId);
            });
}

function fillJoinModal(battleName, battleId, robotId, opponentId) {
    $('#join_battle_modal #battle-name')[0].innerHTML = battleName;
    $('#join_battle_modal #battle-id')[0].innerHTML = battleId;
    $('#join_battle_modal #robot-id')[0].innerHTML = robotId;

    var waitingId;
    var waitingOpponentId;
    var opponentIdElement = $('#join_battle_modal #opponent-name')[0];

    if (opponentId === undefined) {
        var waitingOpponentText = 'Waiting';
        var dot = '.';
        var multiplier = 0;

        waitingId = setInterval(function () {
            opponentIdElement.innerHTML = waitingOpponentText + dot.repeat((multiplier % 5) + 1);
            multiplier++;
        }, 300);

        waitingOpponentId = setInterval(function () {
            $.get('http://PCTOMBASL1:1235/api/battlefield/robotsinbattle?' +
                'battleFieldId=' + battleId,
                function (response) {
                    if (response.length === 1) {
                        clearInterval(waitingId);
                        opponentIdElement.innerHTML = response[0].RobotId;
                        $('#join_battle_modal #join_button')[0].className =
                            $('#join_battle_modal #join_button')[0].className.replace("disabled", "active");;
                        clearInterval(waitingOpponentId);
                    }
                });
        }, 1000);
    } else {
        opponentIdElement.innerHTML = opponentId;
    }
}

function joinBattle(modal) {
    var robotId = sessionStorage.getItem('robotId');
    var battleId = $(modal +' #battle-id')[0].innerHTML || $(modal +' #battle-id')[0].value;
    var battleType = $(modal +' #manual_radio')[0].checked
        ? "manual"
        : "auto";

    $.get('http://PCTOMBASL1:1235/api/battlefield/joinbattle?' +
            'battleId=' + battleId +
            '&robotId=' + robotId +
            '&playType=' + battleType,
            function () {
                waitForBattle(battleId);
            });
}

function waitForBattle(battleFieldId) {
    setInterval(function () {
        $.get('http://PCTOMBASL1:1235/api/battlefield/robotsinbattlecount?' +
            'battleFieldId=' + battleFieldId,
            function (response) {
                if (response === 2) {
                    sessionStorage.setItem("battleFieldId", battleFieldId);
                    window.location.href = '/RobotsAtWar/battle.html';
                }
            });
    }, 1000);
}
