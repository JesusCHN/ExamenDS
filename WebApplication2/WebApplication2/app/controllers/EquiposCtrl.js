angular.module('EquiposController', []).controller('EquiposCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.ListaEquipos = {};
    $scope.ListaGetTecnicos = {};
    $scope.Equipo = {};
    $scope.Accion = 'nuevo';
    $scope.MostrarControles = false;



    $scope.Limpiar = function () {
        $scope.Equipo = {};
        $scope.Accion = 'nuevo';
        $scope.MostrarControles = false;
    };

    $scope.cargar = function () {
        var info = $http.get('/Equipos/GetAll');
        alert(info);
    };

    $scope.NuevoEquipo = function () {
        $scope.Equipo = {};
        $scope.Accion = 'nuevo';
        $scope.MostrarControles = true;
    };

    $scope.EditarEquipo = function (EquipoEditar) {
        $scope.Equipo = EquipoEditar;
        $scope.Accion = 'editar';
        $scope.MostrarControles = true;
    }

    $http.get('/Equipos/GetAll').success(function (data) {
        $scope.ListaEquipos = data;
    });

    $http.get('/Equipos/GetAllTecnico').success(function (data) {
        $scope.ListaGetTecnicos = data;
    });

    $scope.Guardar = function () {
        if ($scope.Accion == 'nuevo') {
            $http.post('/Equipos/Create', $scope.Equipo).success(function (data) {
                $scope.ListaEquipos.push(data);
            });
        }

        if ($scope.Accion == 'editar') {
            $http.post('/Equipos/Update', $scope.Equipo).success(function (data) {
            
            });
        }

        $scope.Limpiar();
    }


    $scope.EliminarEquipo = function (EquipoEliminar) {
        var response = $http({
            method: "post",
            url: "/Equipos/Delete",
            params: { id: JSON.stringify(EquipoEliminar.IdEquipo) }
        });

        var indice = $scope.ListaEquipos.indexOf(EquipoEliminar);

        $scope.ListaEquipos.splice(indice, 1);

    }


}

]);