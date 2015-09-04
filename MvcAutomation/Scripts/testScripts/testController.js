angular.module('tableModule', [])
	.controller('tableController', ['$scope', '$compile', function ($scope, $compile) {
	    var col = 1;
	    var row = 1;

	    var compileTemplate = function (templ, parent, scope) {
	        ($compile(templ)(scope)).fadeIn().appendTo(parent);
	    }

	    $scope.valObject = {};
	    $scope.valObject.GraphArray = [];
	    $scope.valObject.GraphArray[0] = [];
	    $scope.valObject.GraphArray[0][0] = '';

	    $scope.addRow = function () {
	        var trLast = $('<tr></tr>').appendTo('table.automata-table');
	        $scope.valObject.GraphArray[row] = [];

	        for (var i = 0; i != col; i++) {
	            var td = $('<td></td>').appendTo(trLast);
	            if (i == 0) {
	                $scope.valObject.GraphArray[row][i] = false;
	                compileTemplate('<input type="checkbox" ng-model="valObject.GraphArray[' + row + '][' + i + ']"/>q' + (row - 1) + '<br/>', td, $scope);
	            }
	            else {
	                $scope.valObject.GraphArray[row][i] = "";
	                compileTemplate('<input type="text" ng-model="valObject.GraphArray[' + row + '][' + i + ']"/>', td, $scope);
	            }
	        }

	        row++;
	    };

	    $scope.addCol = function () {
	        var trList = $('table.automata-table tr');

	        for (var i = 0; i != row; i++) {
	            $scope.valObject.GraphArray[i][col] = "";
	            compileTemplate('<td><input type="text" ng-model="valObject.GraphArray[' + i + '][' + col + ']"/></td>', trList[i], $scope);
	        }

	        col++;
	    }

	    $scope.delRow = function () {
	        if (row > 1) {
	            $('table.automata-table tr:last').remove();
	            row--;
	        }
	    }

	    $scope.delCol = function () {
	        if (col > 1) {
	            $('table.automata-table tr').each(function (index, el) {
	                $(this).children().last().remove();
	            });
	            col--;
	        }
	    }

	    $scope.sendValues = function () {
	        $scope.valObject.Values = col;
	        $scope.valObject.States = row;
	        var NewTestViewModel = $scope.valObject;
	        var send = function () {
	            $.ajax({
	                type: "POST", url: "CreateTest",
	                success: function (data) {
	                    var d = JSON.parse(data);
	                    if (d.redir == 'Ok')
	                        $.ajax({
	                            url: "Index",
	                            data: null,
	                            success: function (data) {
	                                document.body.innerHTML = data;
	                                alert('Тест успешно создан!');
	                            }
	                        });
	                    else
	                        alert(d.message);
	                },
	                data: JSON.stringify({test: NewTestViewModel}),
	                contentType: 'application/json',
                    dataType: 'html'
	            });
	        }
	        send();
	        console.log($scope.valObject);
	    }
	}]);