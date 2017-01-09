/// <reference path="typings/jquery.cookie/jquery.cookie.d.ts" />
var oauthClient;
(function (oauthClient) {
    function parseResponse(callback) {
        var url = window.location.hash;
        if (url && url !== "") {
            var parameters = getQueryParameters(url);
            var accessToken = parameters.access_token;
            var cookieName = "access_token_cookie";
            var state = parameters.state;
            var returnUrl = state;
            if (state && state !== "") {
                var stateParams = state.split("####");
                if (stateParams.length > 0) {
                    returnUrl = stateParams[0];
                }
            }
            $.cookie(cookieName, accessToken, { secure: true, path: '/' });
            if (returnUrl && returnUrl !== "") {
                window.location.href = returnUrl;
            }
        }
    }
    oauthClient.parseResponse = parseResponse;
    function getQueryParameters(query) {
        if (query[0] === "?" || query[0] === "#") {
            query = query.substr(1, query.length - 1);
        }
        var params = query.split("&");
        var result = [];
        if (params && params.length > 0) {
            for (var i = 0; i < params.length; i++) {
                var values = params[i].split("=");
                if (values.length === 2) {
                    result[values[0]] = decodeURIComponent(values[1]);
                }
            }
        }
        return result;
    }
})(oauthClient || (oauthClient = {}));
//# sourceMappingURL=main.js.map