app = angular.module("admin-app", ["ngRoute"]);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/account", {
            templateUrl: "/admin/account/index.html",
            controller: "account-ctrl"
        })
        .when("/product", {
            templateUrl: "/admin/product/index.html",
            controller: "product-ctrl"
        })
        .when("/category", {
            templateUrl: "/admin/category/index.html",
            controller: "category-ctrl"
        })
        .when("/authorize", {
            templateUrl: "/admin/authority/index.html",
            controller: "authority-ctrl"
        })
        .when("/unauthorized", {
            templateUrl: "/admin/authority/unauthorized.html",
            controller: "authority-ctrl"
        })
        .when("/order", {
            templateUrl: "/admin/order/index.html",
            controller: "order-ctrl"
        })
        .when("/charts", {
            templateUrl: "/admin/charts/index.html",
            controller: "charts-ctrl"
        })
        .when("/voucher", {
            templateUrl: "/admin/voucher/index.html",
            controller: "voucher-ctrl"
        })
        .when("/customer", {
            templateUrl: "/admin/customer/index.html",
            controller: "customer-ctrl"
        })
        .otherwise({
            templateUrl: "/admin/dashboard.html"
        });
});