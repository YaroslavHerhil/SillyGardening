// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $("#closeAuthButton").on('click', function () {
        $("#commentDiv").hide();
        $("#auth").hide();
    });
    $("#openAuthButton").on('click', function () {
        $("#auth").show();
        $(".auth-element").each(function () {
            $(this).show();
        });
    });
    $("#registerButton").on(`click`, function () {
        let userData = {
            UserName: $("#userName").val(),
            Password: $("#userPass").val(),
        }
        $.ajax({
            url: "/Game/Register",
            type: "POST",
            data: userData,
            success: function (response) {
                if (response.isSuccesful) {
                    authorisation(response.user.userName);
                    $("#commentDiv").hide();
                    $("#auth").hide();
                    saveData();
                }
                else {
                    showComment(response.comment);
                }
            }
        })
    })
    $("#loginButton").on(`click`, function () {
        let userData = {
            UserName: $("#userName").val(),
            Password: $("#userPass").val(),
        }
        $.ajax({
            url: "/Game/Login",
            type: "POST",
            data: userData,
            success: function (response) {
                if (response.isSuccesful) {
                    authorisation(response.user.userName);
                    $("#commentDiv").hide();
                    $("#auth").hide();
                    saveData();
                }
                else {
                    showComment(response.comment);
                }
            }
        })
    })
    function showComment(comment) {
        $(".auth-element").each(function () {
            $(this).hide();
        })

        $("#commentDiv").show();
        $("#comment").text(comment);
    }

    

    function authorisation(name) {
        setUpPlots();
        loadPlants();
        updateUI();
        $("#namePlaque").text(name);
        $("#profilePlaque").show();
        $("#openAuthButton").hide();
    }


    function loadPlants() {
        $.ajax({
            url: "/Game/GetGameStats",
            type: "GET",
            success: function (game) {
                game.plants.forEach(plant => {
                    let $button = $(`#buttonplot${plant.id}`);

                    if ($button.length > 0) {
                        setUpSinglePlant($button, plant ,plant.id);
                    }
                });

            }
        });
    }

    function setUpSinglePlant(button, plant, id) {
        $(button).after(`<div class="plant ${plant.type}" id="plant${id}"><img id="face${id}"/></div>`);
        $(button).after(`<div class="progress-bar"><div id="progress${id}" class="plot-progress"></div><span id="progressText${id}" class="progress-text"></span></div>`);
        $(button).after(`<div class="life-progress-bar"><span id="lifeProgressText${id}" class="life-progress-text"></span><div id="lifeProgress${id}" class="life-progress"></div></div>`);
        $(button).remove();
        $(`#face${plant.id}`).attr('src', `../images/faces/face${plant.face}.png`);


        let $plant = $(`#plant${id}`);

        $plant.on("click", function () {
            $.ajax({
                url: "/Game/HugPlant",
                type: "POST",
                data: { id: Number(id) },
                success: function () {
                    updateUI();
                }
            });
        });
    }





    function setUpPlots() {
        $(".plot").each(function () {
            $(this).empty();
            $(this).append(`<button class="plantButton" id="button${this.id}"></button>`);
        });

        $(".plantButton").each(function () {
            $(this).on('click', function () {
                var button = this;
                $.ajax({
                    url: "/Game/Plant",
                    type: "POST",
                    data: { id: Number(button.id.slice(-1)) },
                    success: function (response) {
                        if (response.isSuccesful) {
                            setUpSinglePlant(button, response.plant, button.id.slice(-1));
                        }
                    }
                });
            });
        });

    }


    setUpPlots();

    function explodePlant(plant) {
        $(`#plot${ plant.id }`).empty();
        $(`#plot${plant.id}`).append(`<button class="plantButton" id="buttonplot${plant.id}"></button>`);


        $(`#buttonplot${plant.id}`).on('click', function () {
            var button = this;
            $.ajax({
                url: "/Game/Plant",
                type: "POST",
                data: { id: Number(plant.id) },
                success: function (response) {
                    if (response.isSuccesful) {
                        setUpSinglePlant(button, response.plant, plant.id);
                    }
                }
            });
        });
    }

    function logOut() {
        saveData();
        setUpPlots();
        $("#profilePlaque").hide();
        $("#openAuthButton").show();
    }
    $("#logOutButton").on("click", function () {
        $.ajax({
            url: "/Game/LogOut",
            type: "POST",
            success: logOut
        });
    })


    function saveData() {
            $.ajax({
                url: "/Game/Save",
                type: "POST",
                success: function () {
                    updateUI();
                }
            });
    }

    function updateUI() {
        $.ajax({
            url: "/Game/GetGameStats",
            type: "GET",
            success: function (game) {

                $("#moneyUI").text(game.money.toFixed(2));
                $("#moneyProdUI").text(game.allProduction.toFixed(2));
                $("#plantsUI").text(game.plantCounter);
                if (game.money < 100) {
                    $(".plantButton").each(function () {
                        $(this).hide();
                    })
                }
                else {
                    $(".plantButton").each(function () {
                        $(this).show();
                    })
                }

                game.plants.forEach(function (plant) {

                    $(`#progress${plant.id}`).css("width", `${plant.happiness.toFixed(2) * 100}%`);
                    $(`#lifeProgress${plant.id}`).css("width", `${(((plant.existenceCounter / plant.existenceFinale)).toFixed(2) * 100)}%`);
                    $(`#progressText${plant.id}`).text(`${plant.happiness.toFixed(2) * 100}%`);
                    $(`#lifeProgressText${plant.id}`).text(`${plant.existenceCounter} s`);



                    if (plant.existenceCounter == 0) {
                        debugger
                        $.ajax({
                            url: "/Game/RemovePlant",
                            type: "POST",
                            data: { id: Number(plant.id) },
                            success: function () {
                                explodePlant(plant);
                            }
                        });
                    }
                });


            }
        });
    }






    setInterval(gameTick, 1000);

    function gameTick() {
        $.ajax({
            url: "/Game/GameTick",
            type: "GET",
            success: function () {
                updateUI();


            }
        });
    }

});