var userapp = new angular.module("UsersApp", ['ngRoute']);

userapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'Users/UsersHome',
        controller: 'UsersHomeController'
    }).when('/add', {
        templateUrl: 'Users/UsersAdd',
        controller: 'UsersAddController'
    }).when('/edit', {
        templateUrl: 'Users/UsersEdit',
        controller: 'UsersEditController'
    });
});

userapp.service("sharedDataService", function () {
    this.usersList = [];
    this.usersItem = {};

    this.setUsers = function (obj) { this.usersList = obj; }
    this.getUsers = function () { return this.usersList; }

    this.setItem = function (item) { this.usersItem = item; }
    this.getItem = function () { return this.usersItem; }
});

/* Users Home Controller */
userapp.controller("UsersHomeController", function ($scope, $http, $location, sharedDataService) {
    $scope.UserRoles = {
        avaibleOptions: [],
        selectedOption: {}
    };
    $scope.Users = [];
    $scope.Header = "Управление пользователями";
    $scope.selectForEdit = '';

    /*Helpers*/
    $scope.isActive = function (item) { return $scope.selectForEdit === item; };
    $scope.HighlightItem = function (item) { $scope.selectForEdit = item; };

    /*Method*/
    $scope.AddUser = function () { $location.path('/add'); };
    $scope.EditUser = function () {
        sharedDataService.setItem($scope.selectForEdit);
        $location.path('/edit');
    };
    $scope.RemoveUser = function () {
        $http.post('Users/DeleteUser', { username: $scope.selectForEdit.UserName }).success(function () {
            getAllUsers($scope, $http);
        });
    };

    getAllUsers($scope, $http);
});

/* Users Add Controller */
userapp.controller("UsersAddController", function ($scope, $http, $location) {
    $scope.Header = "Добавление нового пользователя";
    $scope.Roles = [];
    $scope.UserRoles = {
        avaibleOptions: [],
        selectedOption: {}
    };
    $scope.Settings = { selectedRole: [] };

    /* Helpers */
    $scope.toggleSelect = function (name) {
        var pos = $scope.Settings.selectedRole.indexOf(name);
        pos > -1 ? $scope.Settings.selectedRole.splice(pos, 1) : $scope.Settings.selectedRole.push(name);
    }

    /*Methods*/
    $scope.BackToUserList = function () { $location.path('/'); }
    $scope.addUser = function () {
        if ($scope.Settings.selectedRole.length) {
            $('#loader').css({ "display": "block" });
            $('.spinner').show();

            var userModel = {
                UserName: $scope.userAdd.Name,
                Password: $scope.userAdd.Password,
                ConfirmPassword: $scope.userAdd.ConfirmPassword
            };

            $http.post('Users/AddUser', {
                model: userModel,
                roles: $scope.Settings.selectedRole,
                description: $scope.userAdd.Description
            }).success(function (result) {
                $('#loader').css({ "display": "none" });
                $('.spinner').hide();

                if (result === 'ok') {
                    $location.path('/');
                } else {
                    $scope.Error = {
                        Show: true,
                        Class: 'error-wrap',
                        Msg: 'Не удалось создать пользователя!'
                    };
                }
            });

            $('#loader').css({ "display": "none" });
            $('.spinner').hide();
        } else {
            $scope.Rol = {
                Show: true,
                Class: 'error-wrap',
                Msg: 'Необходимо выбрать хотя бы одну роль!'
            };
            return;
        }
    }

    $http.get('Users/GetAllRoles').success(function (response) {
        $scope.Roles = response;
        var temp = [];
        for (var i = 0; i < $scope.Roles.length; ++i) {
            temp[i] = { name: $scope.Roles[i].Name, val: i };
        }
        $scope.UserRoles.avaibleOptions = temp;
    });
});

/* Users Edit Controller */
userapp.controller("UsersEditController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    $scope.Header = "Редактирование учетной записи";
    $scope.itemForEdit = sharedDataService.getItem();
    $scope.itemForEdit.oldname = $scope.itemForEdit.UserName
    $scope.ChangePassword = false;
    $scope.Roles = [];
    $scope.UserRoles = {
        avaibleOptions: [],
        selectedOption: {}
    };
    $scope.Settings = { selectedRole: [] };

    /*Helpers*/
    $scope.ShowPasswordForm = function () { $scope.ChangePassword = true; };
    $scope.BackToUserList = function () { $location.path('/'); };
    $scope.toggleSelect = function (name) {
        var pos = $scope.Settings.selectedRole.indexOf(name);
        pos > -1 ? $scope.Settings.selectedRole.splice(pos, 1) : $scope.Settings.selectedRole.push(name);
    }

    /*Methods*/
    $http.get('Users/GetAllRoles').success(function (response) {
        $scope.Roles = response;
        var temp = [];
        for (var i = 0; i < $scope.Roles.length; ++i) {
            temp[i] = { name: $scope.Roles[i].Name, val: i };
        }
        $scope.UserRoles.avaibleOptions = temp;
    });

    $http.post('Users/GetUserDetail', { username: $scope.itemForEdit.UserName }).success(function (response) {
        if (response.userroles.length > 0) {
            var userRoles = response.userroles,
                userRolesL = userRoles.length;
            for (var i = 0; i < userRolesL; ++i) { $scope.toggleSelect(userRoles[i], true); }
        }
    });

    $scope.changePassword = function () {
        if ($scope.itemForEdit.userPass != $scope.itemForEdit.userPassConfrim) {
            $scope.Pass = {
                Show: true,
                Class: 'error-wrap',
                Msg: 'Пароли не совпадают!'
            };
            return;
        }

        if ($scope.oldPass === '') {
            $scope.Pass = {
                Show: true,
                Class: 'error-wrap',
                Msg: 'Не указан старый пароль!'
            };
            return;
        }

        var userModel = {
            UserName: $scope.itemForEdit.UserName,
            Password: $scope.itemForEdit.Password,
            ConfirmPassword: $scope.itemForEdit.PasswordChange
        };

        $http.post('Users/ChangePassword', { model: userModel, oldPassword: $scope.oldPass }).success(function (response) {
            if (response === 'ok') {
                $scope.Pass = {
                    Show: true,
                    Class: 'succes-wrap',
                    Msg: 'Пароль успешно изменен!'
                };
            } else {
                $scope.Pass = {
                    Show: true,
                    Class: 'error-wrap',
                    Msg: response
                };
            }
        });
    }
    $scope.updateUser = function () {
        if ($scope.Settings.selectedRole.length) {
            if (confirm("Сохранить изменения?")) {
                var temp = {
                    oldname: $scope.itemForEdit.oldname,
                    newname: $scope.itemForEdit.UserName,
                    rolename: $scope.UserRoles.selectedOption.name,
                    description: $scope.itemForEdit.Description,
                    roles: $scope.Settings.selectedRole
                };
                $http.post('Users/UpdateUser', { saveedit: temp }).success(function (response) {
                    $location.path('/');
                });
            }
        } else {
            $scope.Rol = {
                Show: true,
                Class: 'error-wrap',
                Msg: 'Необходимо выбрать хотя бы одну роль!'
            };
            return;
        }
    };
});

function getAllUsers($scope, $http) {
    $('#loader').css({ "display": "block" });
    $('.spinner').show();

    $http.get('Users/GetAllUsers').success(function (result) {
        $scope.Users = result;
    });

    $('#loader').css({ "display": "none" });
    $('.spinner').hide();
}
