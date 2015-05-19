angular.module('TecnicosController', []).controller('TecnicoCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.ListaTecnicos = {};
    $scope.Tecnico = {};
    $scope.Accion = 'nuevo';
    $scope.MostrarControles = false;



    $scope.Limpiar = function () {
        $scope.Tecnico = {};
        $scope.Accion = 'nuevo';
        $scope.MostrarControles = false;
    };

    $scope.cargar = function () {
        var info = $http.get('/Tecnicos/GetAll');
        alert(info);
    };

    $scope.NuevoTecnico = function () {
        $scope.Tecnico = {};
        $scope.Accion = 'nuevo';
        $scope.MostrarControles = true;
    };

    $scope.EditarTecnico = function (TecnicoEditar) {
        $scope.Tecnico = TecnicoEditar;
        $scope.Accion = 'editar';
        $scope.MostrarControles = true;
    }

    $http.get('/Tecnicos/GetAll').success(function (data) {
        $scope.ListaTecnicos = data;
    });


    $scope.Guardar = function () {
        if ($scope.Accion == 'nuevo') {
            $http.post('/Tecnicos/Create', $scope.Tecnico).success(function (data) {
                $scope.ListaTecnicos.push(data);
            });
        }

        if ($scope.Accion == 'editar') {
            $http.post('/Tecnicos/Update', $scope.Tecnico).success(function (data) {

            });
        }

        $scope.Limpiar();
    }


    $scope.EliminarTecnico = function (TecnicoEliminar) {
        var response = $http({
            method: "post",
            url: "/Tecnicos/Delete",
            params: { id: JSON.stringify(TecnicoEliminar.IdTecnico) }
        });

        var indice = $scope.ListaTecnicos.indexOf(TecnicoEliminar);

        $scope.ListaTecnicos.splice(indice, 1);

    }


}

]);