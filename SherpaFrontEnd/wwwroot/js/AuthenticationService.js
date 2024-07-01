/*! For license information please see AuthenticationService.js.LICENSE.txt */
var t, e;
t = {
    671: function(t) {
        var e;
        e = function() {
            return function(t) {
                var e = {};
                function r(n) {
                    if (e[n])
                        return e[n].exports;
                    var i = e[n] = {
                        i: n,
                        l: !1,
                        exports: {}
                    };
                    return t[n].call(i.exports, i, i.exports, r),
                    i.l = !0,
                    i.exports
                }
                return r.m = t,
                r.c = e,
                r.d = function(t, e, n) {
                    r.o(t, e) || Object.defineProperty(t, e, {
                        enumerable: !0,
                        get: n
                    })
                }
                ,
                r.r = function(t) {
                    "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(t, Symbol.toStringTag, {
                        value: "Module"
                    }),
                    Object.defineProperty(t, "__esModule", {
                        value: !0
                    })
                }
                ,
                r.t = function(t, e) {
                    if (1 & e && (t = r(t)),
                    8 & e)
                        return t;
                    if (4 & e && "object" == typeof t && t && t.__esModule)
                        return t;
                    var n = Object.create(null);
                    if (r.r(n),
                    Object.defineProperty(n, "default", {
                        enumerable: !0,
                        value: t
                    }),
                    2 & e && "string" != typeof t)
                        for (var i in t)
                            r.d(n, i, function(e) {
                                return t[e]
                            }
                            .bind(null, i));
                    return n
                }
                ,
                r.n = function(t) {
                    var e = t && t.__esModule ? function() {
                        return t.default
                    }
                    : function() {
                        return t
                    }
                    ;
                    return r.d(e, "a", e),
                    e
                }
                ,
                r.o = function(t, e) {
                    return Object.prototype.hasOwnProperty.call(t, e)
                }
                ,
                r.p = "",
                r(r.s = 22)
            }([function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                });
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = {
                    debug: function() {},
                    info: function() {},
                    warn: function() {},
                    error: function() {}
                }
                  , o = void 0
                  , s = void 0;
                (e.Log = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t.reset = function() {
                        s = 3,
                        o = i
                    }
                    ,
                    t.debug = function() {
                        if (s >= 4) {
                            for (var t = arguments.length, e = Array(t), r = 0; r < t; r++)
                                e[r] = arguments[r];
                            o.debug.apply(o, Array.from(e))
                        }
                    }
                    ,
                    t.info = function() {
                        if (s >= 3) {
                            for (var t = arguments.length, e = Array(t), r = 0; r < t; r++)
                                e[r] = arguments[r];
                            o.info.apply(o, Array.from(e))
                        }
                    }
                    ,
                    t.warn = function() {
                        if (s >= 2) {
                            for (var t = arguments.length, e = Array(t), r = 0; r < t; r++)
                                e[r] = arguments[r];
                            o.warn.apply(o, Array.from(e))
                        }
                    }
                    ,
                    t.error = function() {
                        if (s >= 1) {
                            for (var t = arguments.length, e = Array(t), r = 0; r < t; r++)
                                e[r] = arguments[r];
                            o.error.apply(o, Array.from(e))
                        }
                    }
                    ,
                    n(t, null, [{
                        key: "NONE",
                        get: function() {
                            return 0
                        }
                    }, {
                        key: "ERROR",
                        get: function() {
                            return 1
                        }
                    }, {
                        key: "WARN",
                        get: function() {
                            return 2
                        }
                    }, {
                        key: "INFO",
                        get: function() {
                            return 3
                        }
                    }, {
                        key: "DEBUG",
                        get: function() {
                            return 4
                        }
                    }, {
                        key: "level",
                        get: function() {
                            return s
                        },
                        set: function(t) {
                            if (!(0 <= t && t <= 4))
                                throw new Error("Invalid log level");
                            s = t
                        }
                    }, {
                        key: "logger",
                        get: function() {
                            return o
                        },
                        set: function(t) {
                            if (!t.debug && t.info && (t.debug = t.info),
                            !(t.debug && t.info && t.warn && t.error))
                                throw new Error("Invalid logger");
                            o = t
                        }
                    }]),
                    t
                }()).reset()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                });
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = {
                    setInterval: function(t) {
                        function e(e, r) {
                            return t.apply(this, arguments)
                        }
                        return e.toString = function() {
                            return t.toString()
                        }
                        ,
                        e
                    }((function(t, e) {
                        return setInterval(t, e)
                    }
                    )),
                    clearInterval: function(t) {
                        function e(e) {
                            return t.apply(this, arguments)
                        }
                        return e.toString = function() {
                            return t.toString()
                        }
                        ,
                        e
                    }((function(t) {
                        return clearInterval(t)
                    }
                    ))
                }
                  , o = !1
                  , s = null;
                e.Global = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t._testing = function() {
                        o = !0
                    }
                    ,
                    t.setXMLHttpRequest = function(t) {
                        s = t
                    }
                    ,
                    n(t, null, [{
                        key: "location",
                        get: function() {
                            if (!o)
                                return location
                        }
                    }, {
                        key: "localStorage",
                        get: function() {
                            if (!o && "undefined" != typeof window)
                                return localStorage
                        }
                    }, {
                        key: "sessionStorage",
                        get: function() {
                            if (!o && "undefined" != typeof window)
                                return sessionStorage
                        }
                    }, {
                        key: "XMLHttpRequest",
                        get: function() {
                            if (!o && "undefined" != typeof window)
                                return s || XMLHttpRequest
                        }
                    }, {
                        key: "timer",
                        get: function() {
                            if (!o)
                                return i
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.MetadataService = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0)
                  , o = r(7);
                function s(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                var a = ".well-known/openid-configuration";
                e.MetadataService = function() {
                    function t(e) {
                        var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : o.JsonService;
                        if (s(this, t),
                        !e)
                            throw i.Log.error("MetadataService: No settings passed to MetadataService"),
                            new Error("settings");
                        this._settings = e,
                        this._jsonService = new r(["application/jwk-set+json"])
                    }
                    return t.prototype.resetSigningKeys = function() {
                        this._settings = this._settings || {},
                        this._settings.signingKeys = void 0
                    }
                    ,
                    t.prototype.getMetadata = function() {
                        var t = this;
                        return this._settings.metadata ? (i.Log.debug("MetadataService.getMetadata: Returning metadata from settings"),
                        Promise.resolve(this._settings.metadata)) : this.metadataUrl ? (i.Log.debug("MetadataService.getMetadata: getting metadata from", this.metadataUrl),
                        this._jsonService.getJson(this.metadataUrl).then((function(e) {
                            i.Log.debug("MetadataService.getMetadata: json received");
                            var r = t._settings.metadataSeed || {};
                            return t._settings.metadata = Object.assign({}, r, e),
                            t._settings.metadata
                        }
                        ))) : (i.Log.error("MetadataService.getMetadata: No authority or metadataUrl configured on settings"),
                        Promise.reject(new Error("No authority or metadataUrl configured on settings")))
                    }
                    ,
                    t.prototype.getIssuer = function() {
                        return this._getMetadataProperty("issuer")
                    }
                    ,
                    t.prototype.getAuthorizationEndpoint = function() {
                        return this._getMetadataProperty("authorization_endpoint")
                    }
                    ,
                    t.prototype.getUserInfoEndpoint = function() {
                        return this._getMetadataProperty("userinfo_endpoint")
                    }
                    ,
                    t.prototype.getTokenEndpoint = function() {
                        var t = !(arguments.length > 0 && void 0 !== arguments[0]) || arguments[0];
                        return this._getMetadataProperty("token_endpoint", t)
                    }
                    ,
                    t.prototype.getCheckSessionIframe = function() {
                        return this._getMetadataProperty("check_session_iframe", !0)
                    }
                    ,
                    t.prototype.getEndSessionEndpoint = function() {
                        return this._getMetadataProperty("end_session_endpoint", !0)
                    }
                    ,
                    t.prototype.getRevocationEndpoint = function() {
                        return this._getMetadataProperty("revocation_endpoint", !0)
                    }
                    ,
                    t.prototype.getKeysEndpoint = function() {
                        return this._getMetadataProperty("jwks_uri", !0)
                    }
                    ,
                    t.prototype._getMetadataProperty = function(t) {
                        var e = arguments.length > 1 && void 0 !== arguments[1] && arguments[1];
                        return i.Log.debug("MetadataService.getMetadataProperty for: " + t),
                        this.getMetadata().then((function(r) {
                            if (i.Log.debug("MetadataService.getMetadataProperty: metadata recieved"),
                            void 0 === r[t]) {
                                if (!0 === e)
                                    return void i.Log.warn("MetadataService.getMetadataProperty: Metadata does not contain optional property " + t);
                                throw i.Log.error("MetadataService.getMetadataProperty: Metadata does not contain property " + t),
                                new Error("Metadata does not contain property " + t)
                            }
                            return r[t]
                        }
                        ))
                    }
                    ,
                    t.prototype.getSigningKeys = function() {
                        var t = this;
                        return this._settings.signingKeys ? (i.Log.debug("MetadataService.getSigningKeys: Returning signingKeys from settings"),
                        Promise.resolve(this._settings.signingKeys)) : this._getMetadataProperty("jwks_uri").then((function(e) {
                            return i.Log.debug("MetadataService.getSigningKeys: jwks_uri received", e),
                            t._jsonService.getJson(e).then((function(e) {
                                if (i.Log.debug("MetadataService.getSigningKeys: key set received", e),
                                !e.keys)
                                    throw i.Log.error("MetadataService.getSigningKeys: Missing keys on keyset"),
                                    new Error("Missing keys on keyset");
                                return t._settings.signingKeys = e.keys,
                                t._settings.signingKeys
                            }
                            ))
                        }
                        ))
                    }
                    ,
                    n(t, [{
                        key: "metadataUrl",
                        get: function() {
                            return this._metadataUrl || (this._settings.metadataUrl ? this._metadataUrl = this._settings.metadataUrl : (this._metadataUrl = this._settings.authority,
                            this._metadataUrl && this._metadataUrl.indexOf(a) < 0 && ("/" !== this._metadataUrl[this._metadataUrl.length - 1] && (this._metadataUrl += "/"),
                            this._metadataUrl += a))),
                            this._metadataUrl
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.UrlUtility = void 0;
                var n = r(0)
                  , i = r(1);
                e.UrlUtility = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t.addQueryParam = function(t, e, r) {
                        return t.indexOf("?") < 0 && (t += "?"),
                        "?" !== t[t.length - 1] && (t += "&"),
                        t += encodeURIComponent(e),
                        (t += "=") + encodeURIComponent(r)
                    }
                    ,
                    t.parseUrlFragment = function(t) {
                        var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "#"
                          , r = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : i.Global;
                        "string" != typeof t && (t = r.location.href);
                        var o = t.lastIndexOf(e);
                        o >= 0 && (t = t.substr(o + 1)),
                        "?" === e && (o = t.indexOf("#")) >= 0 && (t = t.substr(0, o));
                        for (var s, a = {}, u = /([^&=]+)=([^&]*)/g, c = 0; s = u.exec(t); )
                            if (a[decodeURIComponent(s[1])] = decodeURIComponent(s[2].replace(/\+/g, " ")),
                            c++ > 50)
                                return n.Log.error("UrlUtility.parseUrlFragment: response exceeded expected number of parameters", t),
                                {
                                    error: "Response exceeded expected number of parameters"
                                };
                        for (var h in a)
                            return a;
                        return {}
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.JoseUtil = void 0;
                var n = r(26)
                  , i = function(t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }(r(33));
                e.JoseUtil = (0,
                i.default)({
                    jws: n.jws,
                    KeyUtil: n.KeyUtil,
                    X509: n.X509,
                    crypto: n.crypto,
                    hextob64u: n.hextob64u,
                    b64tohex: n.b64tohex,
                    AllowedSigningAlgs: n.AllowedSigningAlgs
                })
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.OidcClientSettings = void 0;
                var n = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(t) {
                    return typeof t
                }
                : function(t) {
                    return t && "function" == typeof Symbol && t.constructor === Symbol && t !== Symbol.prototype ? "symbol" : typeof t
                }
                  , i = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , o = r(0)
                  , s = r(23)
                  , a = r(6)
                  , u = r(24)
                  , c = r(2);
                function h(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                var l = ".well-known/openid-configuration"
                  , f = "id_token"
                  , g = "openid"
                  , d = "client_secret_post";
                e.OidcClientSettings = function() {
                    function t() {
                        var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , r = e.authority
                          , i = e.metadataUrl
                          , o = e.metadata
                          , l = e.signingKeys
                          , p = e.metadataSeed
                          , v = e.client_id
                          , y = e.client_secret
                          , m = e.response_type
                          , _ = void 0 === m ? f : m
                          , S = e.scope
                          , w = void 0 === S ? g : S
                          , b = e.redirect_uri
                          , F = e.post_logout_redirect_uri
                          , E = e.client_authentication
                          , x = void 0 === E ? d : E
                          , A = e.prompt
                          , k = e.display
                          , P = e.max_age
                          , C = e.ui_locales
                          , T = e.acr_values
                          , R = e.resource
                          , I = e.response_mode
                          , D = e.filterProtocolClaims
                          , L = void 0 === D || D
                          , N = e.loadUserInfo
                          , U = void 0 === N || N
                          , O = e.staleStateAge
                          , B = void 0 === O ? 900 : O
                          , M = e.clockSkew
                          , j = void 0 === M ? 300 : M
                          , H = e.clockService
                          , K = void 0 === H ? new s.ClockService : H
                          , V = e.userInfoJwtIssuer
                          , q = void 0 === V ? "OP" : V
                          , J = e.mergeClaims
                          , W = void 0 !== J && J
                          , z = e.stateStore
                          , Y = void 0 === z ? new a.WebStorageStateStore : z
                          , G = e.ResponseValidatorCtor
                          , X = void 0 === G ? u.ResponseValidator : G
                          , $ = e.MetadataServiceCtor
                          , Q = void 0 === $ ? c.MetadataService : $
                          , Z = e.extraQueryParams
                          , tt = void 0 === Z ? {} : Z
                          , et = e.extraTokenParams
                          , rt = void 0 === et ? {} : et;
                        h(this, t),
                        this._authority = r,
                        this._metadataUrl = i,
                        this._metadata = o,
                        this._metadataSeed = p,
                        this._signingKeys = l,
                        this._client_id = v,
                        this._client_secret = y,
                        this._response_type = _,
                        this._scope = w,
                        this._redirect_uri = b,
                        this._post_logout_redirect_uri = F,
                        this._client_authentication = x,
                        this._prompt = A,
                        this._display = k,
                        this._max_age = P,
                        this._ui_locales = C,
                        this._acr_values = T,
                        this._resource = R,
                        this._response_mode = I,
                        this._filterProtocolClaims = !!L,
                        this._loadUserInfo = !!U,
                        this._staleStateAge = B,
                        this._clockSkew = j,
                        this._clockService = K,
                        this._userInfoJwtIssuer = q,
                        this._mergeClaims = !!W,
                        this._stateStore = Y,
                        this._validator = new X(this),
                        this._metadataService = new Q(this),
                        this._extraQueryParams = "object" === (void 0 === tt ? "undefined" : n(tt)) ? tt : {},
                        this._extraTokenParams = "object" === (void 0 === rt ? "undefined" : n(rt)) ? rt : {}
                    }
                    return t.prototype.getEpochTime = function() {
                        return this._clockService.getEpochTime()
                    }
                    ,
                    i(t, [{
                        key: "client_id",
                        get: function() {
                            return this._client_id
                        },
                        set: function(t) {
                            if (this._client_id)
                                throw o.Log.error("OidcClientSettings.set_client_id: client_id has already been assigned."),
                                new Error("client_id has already been assigned.");
                            this._client_id = t
                        }
                    }, {
                        key: "client_secret",
                        get: function() {
                            return this._client_secret
                        }
                    }, {
                        key: "response_type",
                        get: function() {
                            return this._response_type
                        }
                    }, {
                        key: "scope",
                        get: function() {
                            return this._scope
                        }
                    }, {
                        key: "redirect_uri",
                        get: function() {
                            return this._redirect_uri
                        }
                    }, {
                        key: "post_logout_redirect_uri",
                        get: function() {
                            return this._post_logout_redirect_uri
                        }
                    }, {
                        key: "client_authentication",
                        get: function() {
                            return this._client_authentication
                        }
                    }, {
                        key: "prompt",
                        get: function() {
                            return this._prompt
                        }
                    }, {
                        key: "display",
                        get: function() {
                            return this._display
                        }
                    }, {
                        key: "max_age",
                        get: function() {
                            return this._max_age
                        }
                    }, {
                        key: "ui_locales",
                        get: function() {
                            return this._ui_locales
                        }
                    }, {
                        key: "acr_values",
                        get: function() {
                            return this._acr_values
                        }
                    }, {
                        key: "resource",
                        get: function() {
                            return this._resource
                        }
                    }, {
                        key: "response_mode",
                        get: function() {
                            return this._response_mode
                        }
                    }, {
                        key: "authority",
                        get: function() {
                            return this._authority
                        },
                        set: function(t) {
                            if (this._authority)
                                throw o.Log.error("OidcClientSettings.set_authority: authority has already been assigned."),
                                new Error("authority has already been assigned.");
                            this._authority = t
                        }
                    }, {
                        key: "metadataUrl",
                        get: function() {
                            return this._metadataUrl || (this._metadataUrl = this.authority,
                            this._metadataUrl && this._metadataUrl.indexOf(l) < 0 && ("/" !== this._metadataUrl[this._metadataUrl.length - 1] && (this._metadataUrl += "/"),
                            this._metadataUrl += l)),
                            this._metadataUrl
                        }
                    }, {
                        key: "metadata",
                        get: function() {
                            return this._metadata
                        },
                        set: function(t) {
                            this._metadata = t
                        }
                    }, {
                        key: "metadataSeed",
                        get: function() {
                            return this._metadataSeed
                        },
                        set: function(t) {
                            this._metadataSeed = t
                        }
                    }, {
                        key: "signingKeys",
                        get: function() {
                            return this._signingKeys
                        },
                        set: function(t) {
                            this._signingKeys = t
                        }
                    }, {
                        key: "filterProtocolClaims",
                        get: function() {
                            return this._filterProtocolClaims
                        }
                    }, {
                        key: "loadUserInfo",
                        get: function() {
                            return this._loadUserInfo
                        }
                    }, {
                        key: "staleStateAge",
                        get: function() {
                            return this._staleStateAge
                        }
                    }, {
                        key: "clockSkew",
                        get: function() {
                            return this._clockSkew
                        }
                    }, {
                        key: "userInfoJwtIssuer",
                        get: function() {
                            return this._userInfoJwtIssuer
                        }
                    }, {
                        key: "mergeClaims",
                        get: function() {
                            return this._mergeClaims
                        }
                    }, {
                        key: "stateStore",
                        get: function() {
                            return this._stateStore
                        }
                    }, {
                        key: "validator",
                        get: function() {
                            return this._validator
                        }
                    }, {
                        key: "metadataService",
                        get: function() {
                            return this._metadataService
                        }
                    }, {
                        key: "extraQueryParams",
                        get: function() {
                            return this._extraQueryParams
                        },
                        set: function(t) {
                            "object" === (void 0 === t ? "undefined" : n(t)) ? this._extraQueryParams = t : this._extraQueryParams = {}
                        }
                    }, {
                        key: "extraTokenParams",
                        get: function() {
                            return this._extraTokenParams
                        },
                        set: function(t) {
                            "object" === (void 0 === t ? "undefined" : n(t)) ? this._extraTokenParams = t : this._extraTokenParams = {}
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.WebStorageStateStore = void 0;
                var n = r(0)
                  , i = r(1);
                function o(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.WebStorageStateStore = function() {
                    function t() {
                        var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , r = e.prefix
                          , n = void 0 === r ? "oidc." : r
                          , s = e.store
                          , a = void 0 === s ? i.Global.localStorage : s;
                        o(this, t),
                        this._store = a,
                        this._prefix = n
                    }
                    return t.prototype.set = function(t, e) {
                        return n.Log.debug("WebStorageStateStore.set", t),
                        t = this._prefix + t,
                        this._store.setItem(t, e),
                        Promise.resolve()
                    }
                    ,
                    t.prototype.get = function(t) {
                        n.Log.debug("WebStorageStateStore.get", t),
                        t = this._prefix + t;
                        var e = this._store.getItem(t);
                        return Promise.resolve(e)
                    }
                    ,
                    t.prototype.remove = function(t) {
                        n.Log.debug("WebStorageStateStore.remove", t),
                        t = this._prefix + t;
                        var e = this._store.getItem(t);
                        return this._store.removeItem(t),
                        Promise.resolve(e)
                    }
                    ,
                    t.prototype.getAllKeys = function() {
                        n.Log.debug("WebStorageStateStore.getAllKeys");
                        for (var t = [], e = 0; e < this._store.length; e++) {
                            var r = this._store.key(e);
                            0 === r.indexOf(this._prefix) && t.push(r.substr(this._prefix.length))
                        }
                        return Promise.resolve(t)
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.JsonService = void 0;
                var n = r(0)
                  , i = r(1);
                function o(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.JsonService = function() {
                    function t() {
                        var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : null
                          , r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : i.Global.XMLHttpRequest
                          , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : null;
                        o(this, t),
                        e && Array.isArray(e) ? this._contentTypes = e.slice() : this._contentTypes = [],
                        this._contentTypes.push("application/json"),
                        n && this._contentTypes.push("application/jwt"),
                        this._XMLHttpRequest = r,
                        this._jwtHandler = n
                    }
                    return t.prototype.getJson = function(t, e) {
                        var r = this;
                        if (!t)
                            throw n.Log.error("JsonService.getJson: No url passed"),
                            new Error("url");
                        return n.Log.debug("JsonService.getJson, url: ", t),
                        new Promise((function(i, o) {
                            var s = new r._XMLHttpRequest;
                            s.open("GET", t);
                            var a = r._contentTypes
                              , u = r._jwtHandler;
                            s.onload = function() {
                                if (n.Log.debug("JsonService.getJson: HTTP response received, status", s.status),
                                200 === s.status) {
                                    var e = s.getResponseHeader("Content-Type");
                                    if (e) {
                                        var r = a.find((function(t) {
                                            if (e.startsWith(t))
                                                return !0
                                        }
                                        ));
                                        if ("application/jwt" == r)
                                            return void u(s).then(i, o);
                                        if (r)
                                            try {
                                                return void i(JSON.parse(s.responseText))
                                            } catch (t) {
                                                return n.Log.error("JsonService.getJson: Error parsing JSON response", t.message),
                                                void o(t)
                                            }
                                    }
                                    o(Error("Invalid response Content-Type: " + e + ", from URL: " + t))
                                } else
                                    o(Error(s.statusText + " (" + s.status + ")"))
                            }
                            ,
                            s.onerror = function() {
                                n.Log.error("JsonService.getJson: network error"),
                                o(Error("Network Error"))
                            }
                            ,
                            e && (n.Log.debug("JsonService.getJson: token passed, setting Authorization header"),
                            s.setRequestHeader("Authorization", "Bearer " + e)),
                            s.send()
                        }
                        ))
                    }
                    ,
                    t.prototype.postForm = function(t, e, r) {
                        var i = this;
                        if (!t)
                            throw n.Log.error("JsonService.postForm: No url passed"),
                            new Error("url");
                        return n.Log.debug("JsonService.postForm, url: ", t),
                        new Promise((function(o, s) {
                            var a = new i._XMLHttpRequest;
                            a.open("POST", t);
                            var u = i._contentTypes;
                            a.onload = function() {
                                if (n.Log.debug("JsonService.postForm: HTTP response received, status", a.status),
                                200 !== a.status) {
                                    if (400 === a.status && (r = a.getResponseHeader("Content-Type")) && u.find((function(t) {
                                        if (r.startsWith(t))
                                            return !0
                                    }
                                    )))
                                        try {
                                            var e = JSON.parse(a.responseText);
                                            if (e && e.error)
                                                return n.Log.error("JsonService.postForm: Error from server: ", e.error),
                                                void s(new Error(e.error))
                                        } catch (t) {
                                            return n.Log.error("JsonService.postForm: Error parsing JSON response", t.message),
                                            void s(t)
                                        }
                                    s(Error(a.statusText + " (" + a.status + ")"))
                                } else {
                                    var r;
                                    if ((r = a.getResponseHeader("Content-Type")) && u.find((function(t) {
                                        if (r.startsWith(t))
                                            return !0
                                    }
                                    )))
                                        try {
                                            return void o(JSON.parse(a.responseText))
                                        } catch (t) {
                                            return n.Log.error("JsonService.postForm: Error parsing JSON response", t.message),
                                            void s(t)
                                        }
                                    s(Error("Invalid response Content-Type: " + r + ", from URL: " + t))
                                }
                            }
                            ,
                            a.onerror = function() {
                                n.Log.error("JsonService.postForm: network error"),
                                s(Error("Network Error"))
                            }
                            ;
                            var c = "";
                            for (var h in e) {
                                var l = e[h];
                                l && (c.length > 0 && (c += "&"),
                                c += encodeURIComponent(h),
                                c += "=",
                                c += encodeURIComponent(l))
                            }
                            a.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"),
                            void 0 !== r && a.setRequestHeader("Authorization", "Basic " + btoa(r)),
                            a.send(c)
                        }
                        ))
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.SigninRequest = void 0;
                var n = r(0)
                  , i = r(3)
                  , o = r(13);
                e.SigninRequest = function() {
                    function t(e) {
                        var r = e.url
                          , s = e.client_id
                          , a = e.redirect_uri
                          , u = e.response_type
                          , c = e.scope
                          , h = e.authority
                          , l = e.data
                          , f = e.prompt
                          , g = e.display
                          , d = e.max_age
                          , p = e.ui_locales
                          , v = e.id_token_hint
                          , y = e.login_hint
                          , m = e.acr_values
                          , _ = e.resource
                          , S = e.response_mode
                          , w = e.request
                          , b = e.request_uri
                          , F = e.extraQueryParams
                          , E = e.request_type
                          , x = e.client_secret
                          , A = e.extraTokenParams
                          , k = e.skipUserInfo;
                        if (function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t),
                        !r)
                            throw n.Log.error("SigninRequest.ctor: No url passed"),
                            new Error("url");
                        if (!s)
                            throw n.Log.error("SigninRequest.ctor: No client_id passed"),
                            new Error("client_id");
                        if (!a)
                            throw n.Log.error("SigninRequest.ctor: No redirect_uri passed"),
                            new Error("redirect_uri");
                        if (!u)
                            throw n.Log.error("SigninRequest.ctor: No response_type passed"),
                            new Error("response_type");
                        if (!c)
                            throw n.Log.error("SigninRequest.ctor: No scope passed"),
                            new Error("scope");
                        if (!h)
                            throw n.Log.error("SigninRequest.ctor: No authority passed"),
                            new Error("authority");
                        var P = t.isOidc(u)
                          , C = t.isCode(u);
                        S || (S = t.isCode(u) ? "query" : null),
                        this.state = new o.SigninState({
                            nonce: P,
                            data: l,
                            client_id: s,
                            authority: h,
                            redirect_uri: a,
                            code_verifier: C,
                            request_type: E,
                            response_mode: S,
                            client_secret: x,
                            scope: c,
                            extraTokenParams: A,
                            skipUserInfo: k
                        }),
                        r = i.UrlUtility.addQueryParam(r, "client_id", s),
                        r = i.UrlUtility.addQueryParam(r, "redirect_uri", a),
                        r = i.UrlUtility.addQueryParam(r, "response_type", u),
                        r = i.UrlUtility.addQueryParam(r, "scope", c),
                        r = i.UrlUtility.addQueryParam(r, "state", this.state.id),
                        P && (r = i.UrlUtility.addQueryParam(r, "nonce", this.state.nonce)),
                        C && (r = i.UrlUtility.addQueryParam(r, "code_challenge", this.state.code_challenge),
                        r = i.UrlUtility.addQueryParam(r, "code_challenge_method", "S256"));
                        var T = {
                            prompt: f,
                            display: g,
                            max_age: d,
                            ui_locales: p,
                            id_token_hint: v,
                            login_hint: y,
                            acr_values: m,
                            resource: _,
                            request: w,
                            request_uri: b,
                            response_mode: S
                        };
                        for (var R in T)
                            T[R] && (r = i.UrlUtility.addQueryParam(r, R, T[R]));
                        for (var I in F)
                            r = i.UrlUtility.addQueryParam(r, I, F[I]);
                        this.url = r
                    }
                    return t.isOidc = function(t) {
                        return !!t.split(/\s+/g).filter((function(t) {
                            return "id_token" === t
                        }
                        ))[0]
                    }
                    ,
                    t.isOAuth = function(t) {
                        return !!t.split(/\s+/g).filter((function(t) {
                            return "token" === t
                        }
                        ))[0]
                    }
                    ,
                    t.isCode = function(t) {
                        return !!t.split(/\s+/g).filter((function(t) {
                            return "code" === t
                        }
                        ))[0]
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.State = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0)
                  , o = function(t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }(r(14));
                function s(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.State = function() {
                    function t() {
                        var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , r = e.id
                          , n = e.data
                          , i = e.created
                          , a = e.request_type;
                        s(this, t),
                        this._id = r || (0,
                        o.default)(),
                        this._data = n,
                        this._created = "number" == typeof i && i > 0 ? i : parseInt(Date.now() / 1e3),
                        this._request_type = a
                    }
                    return t.prototype.toStorageString = function() {
                        return i.Log.debug("State.toStorageString"),
                        JSON.stringify({
                            id: this.id,
                            data: this.data,
                            created: this.created,
                            request_type: this.request_type
                        })
                    }
                    ,
                    t.fromStorageString = function(e) {
                        return i.Log.debug("State.fromStorageString"),
                        new t(JSON.parse(e))
                    }
                    ,
                    t.clearStaleState = function(e, r) {
                        var n = Date.now() / 1e3 - r;
                        return e.getAllKeys().then((function(r) {
                            i.Log.debug("State.clearStaleState: got keys", r);
                            for (var o = [], s = function(s) {
                                var a = r[s];
                                u = e.get(a).then((function(r) {
                                    var o = !1;
                                    if (r)
                                        try {
                                            var s = t.fromStorageString(r);
                                            i.Log.debug("State.clearStaleState: got item from key: ", a, s.created),
                                            s.created <= n && (o = !0)
                                        } catch (t) {
                                            i.Log.error("State.clearStaleState: Error parsing state for key", a, t.message),
                                            o = !0
                                        }
                                    else
                                        i.Log.debug("State.clearStaleState: no item in storage for key: ", a),
                                        o = !0;
                                    if (o)
                                        return i.Log.debug("State.clearStaleState: removed item for key: ", a),
                                        e.remove(a)
                                }
                                )),
                                o.push(u)
                            }, a = 0; a < r.length; a++) {
                                var u;
                                s(a)
                            }
                            return i.Log.debug("State.clearStaleState: waiting on promise count:", o.length),
                            Promise.all(o)
                        }
                        ))
                    }
                    ,
                    n(t, [{
                        key: "id",
                        get: function() {
                            return this._id
                        }
                    }, {
                        key: "data",
                        get: function() {
                            return this._data
                        }
                    }, {
                        key: "created",
                        get: function() {
                            return this._created
                        }
                    }, {
                        key: "request_type",
                        get: function() {
                            return this._request_type
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.OidcClient = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0)
                  , o = r(5)
                  , s = r(12)
                  , a = r(8)
                  , u = r(34)
                  , c = r(35)
                  , h = r(36)
                  , l = r(13)
                  , f = r(9);
                function g(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.OidcClient = function() {
                    function t() {
                        var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        g(this, t),
                        e instanceof o.OidcClientSettings ? this._settings = e : this._settings = new o.OidcClientSettings(e)
                    }
                    return t.prototype.createSigninRequest = function() {
                        var t = this
                          , e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , r = e.response_type
                          , n = e.scope
                          , o = e.redirect_uri
                          , s = e.data
                          , u = e.state
                          , c = e.prompt
                          , h = e.display
                          , l = e.max_age
                          , f = e.ui_locales
                          , g = e.id_token_hint
                          , d = e.login_hint
                          , p = e.acr_values
                          , v = e.resource
                          , y = e.request
                          , m = e.request_uri
                          , _ = e.response_mode
                          , S = e.extraQueryParams
                          , w = e.extraTokenParams
                          , b = e.request_type
                          , F = e.skipUserInfo
                          , E = arguments[1];
                        i.Log.debug("OidcClient.createSigninRequest");
                        var x = this._settings.client_id;
                        r = r || this._settings.response_type,
                        n = n || this._settings.scope,
                        o = o || this._settings.redirect_uri,
                        c = c || this._settings.prompt,
                        h = h || this._settings.display,
                        l = l || this._settings.max_age,
                        f = f || this._settings.ui_locales,
                        p = p || this._settings.acr_values,
                        v = v || this._settings.resource,
                        _ = _ || this._settings.response_mode,
                        S = S || this._settings.extraQueryParams,
                        w = w || this._settings.extraTokenParams;
                        var A = this._settings.authority;
                        return a.SigninRequest.isCode(r) && "code" !== r ? Promise.reject(new Error("OpenID Connect hybrid flow is not supported")) : this._metadataService.getAuthorizationEndpoint().then((function(e) {
                            i.Log.debug("OidcClient.createSigninRequest: Received authorization endpoint", e);
                            var k = new a.SigninRequest({
                                url: e,
                                client_id: x,
                                redirect_uri: o,
                                response_type: r,
                                scope: n,
                                data: s || u,
                                authority: A,
                                prompt: c,
                                display: h,
                                max_age: l,
                                ui_locales: f,
                                id_token_hint: g,
                                login_hint: d,
                                acr_values: p,
                                resource: v,
                                request: y,
                                request_uri: m,
                                extraQueryParams: S,
                                extraTokenParams: w,
                                request_type: b,
                                response_mode: _,
                                client_secret: t._settings.client_secret,
                                skipUserInfo: F
                            })
                              , P = k.state;
                            return (E = E || t._stateStore).set(P.id, P.toStorageString()).then((function() {
                                return k
                            }
                            ))
                        }
                        ))
                    }
                    ,
                    t.prototype.readSigninResponseState = function(t, e) {
                        var r = arguments.length > 2 && void 0 !== arguments[2] && arguments[2];
                        i.Log.debug("OidcClient.readSigninResponseState");
                        var n = "query" === this._settings.response_mode || !this._settings.response_mode && a.SigninRequest.isCode(this._settings.response_type)
                          , o = n ? "?" : "#"
                          , s = new u.SigninResponse(t,o);
                        if (!s.state)
                            return i.Log.error("OidcClient.readSigninResponseState: No state in response"),
                            Promise.reject(new Error("No state in response"));
                        e = e || this._stateStore;
                        var c = r ? e.remove.bind(e) : e.get.bind(e);
                        return c(s.state).then((function(t) {
                            if (!t)
                                throw i.Log.error("OidcClient.readSigninResponseState: No matching state found in storage"),
                                new Error("No matching state found in storage");
                            return {
                                state: l.SigninState.fromStorageString(t),
                                response: s
                            }
                        }
                        ))
                    }
                    ,
                    t.prototype.processSigninResponse = function(t, e) {
                        var r = this;
                        return i.Log.debug("OidcClient.processSigninResponse"),
                        this.readSigninResponseState(t, e, !0).then((function(t) {
                            var e = t.state
                              , n = t.response;
                            return i.Log.debug("OidcClient.processSigninResponse: Received state from storage; validating response"),
                            r._validator.validateSigninResponse(e, n)
                        }
                        ))
                    }
                    ,
                    t.prototype.createSignoutRequest = function() {
                        var t = this
                          , e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , r = e.id_token_hint
                          , n = e.data
                          , o = e.state
                          , s = e.post_logout_redirect_uri
                          , a = e.extraQueryParams
                          , u = e.request_type
                          , h = arguments[1];
                        return i.Log.debug("OidcClient.createSignoutRequest"),
                        s = s || this._settings.post_logout_redirect_uri,
                        a = a || this._settings.extraQueryParams,
                        this._metadataService.getEndSessionEndpoint().then((function(e) {
                            if (!e)
                                throw i.Log.error("OidcClient.createSignoutRequest: No end session endpoint url returned"),
                                new Error("no end session endpoint");
                            i.Log.debug("OidcClient.createSignoutRequest: Received end session endpoint", e);
                            var l = new c.SignoutRequest({
                                url: e,
                                id_token_hint: r,
                                post_logout_redirect_uri: s,
                                data: n || o,
                                extraQueryParams: a,
                                request_type: u
                            })
                              , f = l.state;
                            return f && (i.Log.debug("OidcClient.createSignoutRequest: Signout request has state to persist"),
                            (h = h || t._stateStore).set(f.id, f.toStorageString())),
                            l
                        }
                        ))
                    }
                    ,
                    t.prototype.readSignoutResponseState = function(t, e) {
                        var r = arguments.length > 2 && void 0 !== arguments[2] && arguments[2];
                        i.Log.debug("OidcClient.readSignoutResponseState");
                        var n = new h.SignoutResponse(t);
                        if (!n.state)
                            return i.Log.debug("OidcClient.readSignoutResponseState: No state in response"),
                            n.error ? (i.Log.warn("OidcClient.readSignoutResponseState: Response was error: ", n.error),
                            Promise.reject(new s.ErrorResponse(n))) : Promise.resolve({
                                state: void 0,
                                response: n
                            });
                        var o = n.state;
                        e = e || this._stateStore;
                        var a = r ? e.remove.bind(e) : e.get.bind(e);
                        return a(o).then((function(t) {
                            if (!t)
                                throw i.Log.error("OidcClient.readSignoutResponseState: No matching state found in storage"),
                                new Error("No matching state found in storage");
                            return {
                                state: f.State.fromStorageString(t),
                                response: n
                            }
                        }
                        ))
                    }
                    ,
                    t.prototype.processSignoutResponse = function(t, e) {
                        var r = this;
                        return i.Log.debug("OidcClient.processSignoutResponse"),
                        this.readSignoutResponseState(t, e, !0).then((function(t) {
                            var e = t.state
                              , n = t.response;
                            return e ? (i.Log.debug("OidcClient.processSignoutResponse: Received state from storage; validating response"),
                            r._validator.validateSignoutResponse(e, n)) : (i.Log.debug("OidcClient.processSignoutResponse: No state from storage; skipping validating response"),
                            n)
                        }
                        ))
                    }
                    ,
                    t.prototype.clearStaleState = function(t) {
                        return i.Log.debug("OidcClient.clearStaleState"),
                        t = t || this._stateStore,
                        f.State.clearStaleState(t, this.settings.staleStateAge)
                    }
                    ,
                    n(t, [{
                        key: "_stateStore",
                        get: function() {
                            return this.settings.stateStore
                        }
                    }, {
                        key: "_validator",
                        get: function() {
                            return this.settings.validator
                        }
                    }, {
                        key: "_metadataService",
                        get: function() {
                            return this.settings.metadataService
                        }
                    }, {
                        key: "settings",
                        get: function() {
                            return this._settings
                        }
                    }, {
                        key: "metadataService",
                        get: function() {
                            return this._metadataService
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.TokenClient = void 0;
                var n = r(7)
                  , i = r(2)
                  , o = r(0);
                function s(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.TokenClient = function() {
                    function t(e) {
                        var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : n.JsonService
                          , a = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : i.MetadataService;
                        if (s(this, t),
                        !e)
                            throw o.Log.error("TokenClient.ctor: No settings passed"),
                            new Error("settings");
                        this._settings = e,
                        this._jsonService = new r,
                        this._metadataService = new a(this._settings)
                    }
                    return t.prototype.exchangeCode = function() {
                        var t = this
                          , e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        (e = Object.assign({}, e)).grant_type = e.grant_type || "authorization_code",
                        e.client_id = e.client_id || this._settings.client_id,
                        e.client_secret = e.client_secret || this._settings.client_secret,
                        e.redirect_uri = e.redirect_uri || this._settings.redirect_uri;
                        var r = void 0
                          , n = e._client_authentication || this._settings._client_authentication;
                        return delete e._client_authentication,
                        e.code ? e.redirect_uri ? e.code_verifier ? e.client_id ? e.client_secret || "client_secret_basic" != n ? ("client_secret_basic" == n && (r = e.client_id + ":" + e.client_secret,
                        delete e.client_id,
                        delete e.client_secret),
                        this._metadataService.getTokenEndpoint(!1).then((function(n) {
                            return o.Log.debug("TokenClient.exchangeCode: Received token endpoint"),
                            t._jsonService.postForm(n, e, r).then((function(t) {
                                return o.Log.debug("TokenClient.exchangeCode: response received"),
                                t
                            }
                            ))
                        }
                        ))) : (o.Log.error("TokenClient.exchangeCode: No client_secret passed"),
                        Promise.reject(new Error("A client_secret is required"))) : (o.Log.error("TokenClient.exchangeCode: No client_id passed"),
                        Promise.reject(new Error("A client_id is required"))) : (o.Log.error("TokenClient.exchangeCode: No code_verifier passed"),
                        Promise.reject(new Error("A code_verifier is required"))) : (o.Log.error("TokenClient.exchangeCode: No redirect_uri passed"),
                        Promise.reject(new Error("A redirect_uri is required"))) : (o.Log.error("TokenClient.exchangeCode: No code passed"),
                        Promise.reject(new Error("A code is required")))
                    }
                    ,
                    t.prototype.exchangeRefreshToken = function() {
                        var t = this
                          , e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        (e = Object.assign({}, e)).grant_type = e.grant_type || "refresh_token",
                        e.client_id = e.client_id || this._settings.client_id,
                        e.client_secret = e.client_secret || this._settings.client_secret;
                        var r = void 0
                          , n = e._client_authentication || this._settings._client_authentication;
                        return delete e._client_authentication,
                        e.refresh_token ? e.client_id ? ("client_secret_basic" == n && (r = e.client_id + ":" + e.client_secret,
                        delete e.client_id,
                        delete e.client_secret),
                        this._metadataService.getTokenEndpoint(!1).then((function(n) {
                            return o.Log.debug("TokenClient.exchangeRefreshToken: Received token endpoint"),
                            t._jsonService.postForm(n, e, r).then((function(t) {
                                return o.Log.debug("TokenClient.exchangeRefreshToken: response received"),
                                t
                            }
                            ))
                        }
                        ))) : (o.Log.error("TokenClient.exchangeRefreshToken: No client_id passed"),
                        Promise.reject(new Error("A client_id is required"))) : (o.Log.error("TokenClient.exchangeRefreshToken: No refresh_token passed"),
                        Promise.reject(new Error("A refresh_token is required")))
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.ErrorResponse = void 0;
                var n = r(0);
                function i(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                function o(t, e) {
                    if (!t)
                        throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                    return !e || "object" != typeof e && "function" != typeof e ? t : e
                }
                e.ErrorResponse = function(t) {
                    function e() {
                        var r = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , s = r.error
                          , a = r.error_description
                          , u = r.error_uri
                          , c = r.state
                          , h = r.session_state;
                        if (i(this, e),
                        !s)
                            throw n.Log.error("No error passed to ErrorResponse"),
                            new Error("error");
                        var l = o(this, t.call(this, a || s));
                        return l.name = "ErrorResponse",
                        l.error = s,
                        l.error_description = a,
                        l.error_uri = u,
                        l.state = c,
                        l.session_state = h,
                        l
                    }
                    return function(t, e) {
                        if ("function" != typeof e && null !== e)
                            throw new TypeError("Super expression must either be null or a function, not " + typeof e);
                        t.prototype = Object.create(e && e.prototype, {
                            constructor: {
                                value: t,
                                enumerable: !1,
                                writable: !0,
                                configurable: !0
                            }
                        }),
                        e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
                    }(e, t),
                    e
                }(Error)
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.SigninState = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0)
                  , o = r(9)
                  , s = r(4)
                  , a = function(t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }(r(14));
                function u(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                function c(t, e) {
                    if (!t)
                        throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                    return !e || "object" != typeof e && "function" != typeof e ? t : e
                }
                e.SigninState = function(t) {
                    function e() {
                        var r = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , n = r.nonce
                          , i = r.authority
                          , o = r.client_id
                          , h = r.redirect_uri
                          , l = r.code_verifier
                          , f = r.response_mode
                          , g = r.client_secret
                          , d = r.scope
                          , p = r.extraTokenParams
                          , v = r.skipUserInfo;
                        u(this, e);
                        var y = c(this, t.call(this, arguments[0]));
                        if (!0 === n ? y._nonce = (0,
                        a.default)() : n && (y._nonce = n),
                        !0 === l ? y._code_verifier = (0,
                        a.default)() + (0,
                        a.default)() + (0,
                        a.default)() : l && (y._code_verifier = l),
                        y.code_verifier) {
                            var m = s.JoseUtil.hashString(y.code_verifier, "SHA256");
                            y._code_challenge = s.JoseUtil.hexToBase64Url(m)
                        }
                        return y._redirect_uri = h,
                        y._authority = i,
                        y._client_id = o,
                        y._response_mode = f,
                        y._client_secret = g,
                        y._scope = d,
                        y._extraTokenParams = p,
                        y._skipUserInfo = v,
                        y
                    }
                    return function(t, e) {
                        if ("function" != typeof e && null !== e)
                            throw new TypeError("Super expression must either be null or a function, not " + typeof e);
                        t.prototype = Object.create(e && e.prototype, {
                            constructor: {
                                value: t,
                                enumerable: !1,
                                writable: !0,
                                configurable: !0
                            }
                        }),
                        e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
                    }(e, t),
                    e.prototype.toStorageString = function() {
                        return i.Log.debug("SigninState.toStorageString"),
                        JSON.stringify({
                            id: this.id,
                            data: this.data,
                            created: this.created,
                            request_type: this.request_type,
                            nonce: this.nonce,
                            code_verifier: this.code_verifier,
                            redirect_uri: this.redirect_uri,
                            authority: this.authority,
                            client_id: this.client_id,
                            response_mode: this.response_mode,
                            client_secret: this.client_secret,
                            scope: this.scope,
                            extraTokenParams: this.extraTokenParams,
                            skipUserInfo: this.skipUserInfo
                        })
                    }
                    ,
                    e.fromStorageString = function(t) {
                        return i.Log.debug("SigninState.fromStorageString"),
                        new e(JSON.parse(t))
                    }
                    ,
                    n(e, [{
                        key: "nonce",
                        get: function() {
                            return this._nonce
                        }
                    }, {
                        key: "authority",
                        get: function() {
                            return this._authority
                        }
                    }, {
                        key: "client_id",
                        get: function() {
                            return this._client_id
                        }
                    }, {
                        key: "redirect_uri",
                        get: function() {
                            return this._redirect_uri
                        }
                    }, {
                        key: "code_verifier",
                        get: function() {
                            return this._code_verifier
                        }
                    }, {
                        key: "code_challenge",
                        get: function() {
                            return this._code_challenge
                        }
                    }, {
                        key: "response_mode",
                        get: function() {
                            return this._response_mode
                        }
                    }, {
                        key: "client_secret",
                        get: function() {
                            return this._client_secret
                        }
                    }, {
                        key: "scope",
                        get: function() {
                            return this._scope
                        }
                    }, {
                        key: "extraTokenParams",
                        get: function() {
                            return this._extraTokenParams
                        }
                    }, {
                        key: "skipUserInfo",
                        get: function() {
                            return this._skipUserInfo
                        }
                    }]),
                    e
                }(o.State)
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.default = function() {
                    return ("undefined" != n && null !== n && void 0 !== n.getRandomValues ? i : o)().replace(/-/g, "")
                }
                ;
                var n = "undefined" != typeof window ? window.crypto || window.msCrypto : null;
                function i() {
                    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (function(t) {
                        return (t ^ n.getRandomValues(new Uint8Array(1))[0] & 15 >> t / 4).toString(16)
                    }
                    ))
                }
                function o() {
                    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, (function(t) {
                        return (t ^ 16 * Math.random() >> t / 4).toString(16)
                    }
                    ))
                }
                t.exports = e.default
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.User = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0);
                e.User = function() {
                    function t(e) {
                        var r = e.id_token
                          , n = e.session_state
                          , i = e.access_token
                          , o = e.refresh_token
                          , s = e.token_type
                          , a = e.scope
                          , u = e.profile
                          , c = e.expires_at
                          , h = e.state;
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t),
                        this.id_token = r,
                        this.session_state = n,
                        this.access_token = i,
                        this.refresh_token = o,
                        this.token_type = s,
                        this.scope = a,
                        this.profile = u,
                        this.expires_at = c,
                        this.state = h
                    }
                    return t.prototype.toStorageString = function() {
                        return i.Log.debug("User.toStorageString"),
                        JSON.stringify({
                            id_token: this.id_token,
                            session_state: this.session_state,
                            access_token: this.access_token,
                            refresh_token: this.refresh_token,
                            token_type: this.token_type,
                            scope: this.scope,
                            profile: this.profile,
                            expires_at: this.expires_at
                        })
                    }
                    ,
                    t.fromStorageString = function(e) {
                        return i.Log.debug("User.fromStorageString"),
                        new t(JSON.parse(e))
                    }
                    ,
                    n(t, [{
                        key: "expires_in",
                        get: function() {
                            if (this.expires_at) {
                                var t = parseInt(Date.now() / 1e3);
                                return this.expires_at - t
                            }
                        },
                        set: function(t) {
                            var e = parseInt(t);
                            if ("number" == typeof e && e > 0) {
                                var r = parseInt(Date.now() / 1e3);
                                this.expires_at = r + e
                            }
                        }
                    }, {
                        key: "expired",
                        get: function() {
                            var t = this.expires_in;
                            if (void 0 !== t)
                                return t <= 0
                        }
                    }, {
                        key: "scopes",
                        get: function() {
                            return (this.scope || "").split(" ")
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.AccessTokenEvents = void 0;
                var n = r(0)
                  , i = r(46);
                function o(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.AccessTokenEvents = function() {
                    function t() {
                        var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , r = e.accessTokenExpiringNotificationTime
                          , n = void 0 === r ? 60 : r
                          , s = e.accessTokenExpiringTimer
                          , a = void 0 === s ? new i.Timer("Access token expiring") : s
                          , u = e.accessTokenExpiredTimer
                          , c = void 0 === u ? new i.Timer("Access token expired") : u;
                        o(this, t),
                        this._accessTokenExpiringNotificationTime = n,
                        this._accessTokenExpiring = a,
                        this._accessTokenExpired = c
                    }
                    return t.prototype.load = function(t) {
                        if (t.access_token && void 0 !== t.expires_in) {
                            var e = t.expires_in;
                            if (n.Log.debug("AccessTokenEvents.load: access token present, remaining duration:", e),
                            e > 0) {
                                var r = e - this._accessTokenExpiringNotificationTime;
                                r <= 0 && (r = 1),
                                n.Log.debug("AccessTokenEvents.load: registering expiring timer in:", r),
                                this._accessTokenExpiring.init(r)
                            } else
                                n.Log.debug("AccessTokenEvents.load: canceling existing expiring timer becase we're past expiration."),
                                this._accessTokenExpiring.cancel();
                            var i = e + 1;
                            n.Log.debug("AccessTokenEvents.load: registering expired timer in:", i),
                            this._accessTokenExpired.init(i)
                        } else
                            this._accessTokenExpiring.cancel(),
                            this._accessTokenExpired.cancel()
                    }
                    ,
                    t.prototype.unload = function() {
                        n.Log.debug("AccessTokenEvents.unload: canceling existing access token timers"),
                        this._accessTokenExpiring.cancel(),
                        this._accessTokenExpired.cancel()
                    }
                    ,
                    t.prototype.addAccessTokenExpiring = function(t) {
                        this._accessTokenExpiring.addHandler(t)
                    }
                    ,
                    t.prototype.removeAccessTokenExpiring = function(t) {
                        this._accessTokenExpiring.removeHandler(t)
                    }
                    ,
                    t.prototype.addAccessTokenExpired = function(t) {
                        this._accessTokenExpired.addHandler(t)
                    }
                    ,
                    t.prototype.removeAccessTokenExpired = function(t) {
                        this._accessTokenExpired.removeHandler(t)
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.Event = void 0;
                var n = r(0);
                e.Event = function() {
                    function t(e) {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t),
                        this._name = e,
                        this._callbacks = []
                    }
                    return t.prototype.addHandler = function(t) {
                        this._callbacks.push(t)
                    }
                    ,
                    t.prototype.removeHandler = function(t) {
                        var e = this._callbacks.findIndex((function(e) {
                            return e === t
                        }
                        ));
                        e >= 0 && this._callbacks.splice(e, 1)
                    }
                    ,
                    t.prototype.raise = function() {
                        n.Log.debug("Event: Raising event: " + this._name);
                        for (var t = 0; t < this._callbacks.length; t++) {
                            var e;
                            (e = this._callbacks)[t].apply(e, arguments)
                        }
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.SessionMonitor = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0)
                  , o = r(19)
                  , s = r(1);
                function a(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.SessionMonitor = function() {
                    function t(e) {
                        var r = this
                          , n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : o.CheckSessionIFrame
                          , u = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : s.Global.timer;
                        if (a(this, t),
                        !e)
                            throw i.Log.error("SessionMonitor.ctor: No user manager passed to SessionMonitor"),
                            new Error("userManager");
                        this._userManager = e,
                        this._CheckSessionIFrameCtor = n,
                        this._timer = u,
                        this._userManager.events.addUserLoaded(this._start.bind(this)),
                        this._userManager.events.addUserUnloaded(this._stop.bind(this)),
                        Promise.resolve(this._userManager.getUser().then((function(t) {
                            t ? r._start(t) : r._settings.monitorAnonymousSession && r._userManager.querySessionStatus().then((function(t) {
                                var e = {
                                    session_state: t.session_state
                                };
                                t.sub && t.sid && (e.profile = {
                                    sub: t.sub,
                                    sid: t.sid
                                }),
                                r._start(e)
                            }
                            )).catch((function(t) {
                                i.Log.error("SessionMonitor ctor: error from querySessionStatus:", t.message)
                            }
                            ))
                        }
                        )).catch((function(t) {
                            i.Log.error("SessionMonitor ctor: error from getUser:", t.message)
                        }
                        )))
                    }
                    return t.prototype._start = function(t) {
                        var e = this
                          , r = t.session_state;
                        r && (t.profile ? (this._sub = t.profile.sub,
                        this._sid = t.profile.sid,
                        i.Log.debug("SessionMonitor._start: session_state:", r, ", sub:", this._sub)) : (this._sub = void 0,
                        this._sid = void 0,
                        i.Log.debug("SessionMonitor._start: session_state:", r, ", anonymous user")),
                        this._checkSessionIFrame ? this._checkSessionIFrame.start(r) : this._metadataService.getCheckSessionIframe().then((function(t) {
                            if (t) {
                                i.Log.debug("SessionMonitor._start: Initializing check session iframe");
                                var n = e._client_id
                                  , o = e._checkSessionInterval
                                  , s = e._stopCheckSessionOnError;
                                e._checkSessionIFrame = new e._CheckSessionIFrameCtor(e._callback.bind(e),n,t,o,s),
                                e._checkSessionIFrame.load().then((function() {
                                    e._checkSessionIFrame.start(r)
                                }
                                ))
                            } else
                                i.Log.warn("SessionMonitor._start: No check session iframe found in the metadata")
                        }
                        )).catch((function(t) {
                            i.Log.error("SessionMonitor._start: Error from getCheckSessionIframe:", t.message)
                        }
                        )))
                    }
                    ,
                    t.prototype._stop = function() {
                        var t = this;
                        if (this._sub = void 0,
                        this._sid = void 0,
                        this._checkSessionIFrame && (i.Log.debug("SessionMonitor._stop"),
                        this._checkSessionIFrame.stop()),
                        this._settings.monitorAnonymousSession)
                            var e = this._timer.setInterval((function() {
                                t._timer.clearInterval(e),
                                t._userManager.querySessionStatus().then((function(e) {
                                    var r = {
                                        session_state: e.session_state
                                    };
                                    e.sub && e.sid && (r.profile = {
                                        sub: e.sub,
                                        sid: e.sid
                                    }),
                                    t._start(r)
                                }
                                )).catch((function(t) {
                                    i.Log.error("SessionMonitor: error from querySessionStatus:", t.message)
                                }
                                ))
                            }
                            ), 1e3)
                    }
                    ,
                    t.prototype._callback = function() {
                        var t = this;
                        this._userManager.querySessionStatus().then((function(e) {
                            var r = !0;
                            e ? e.sub === t._sub ? (r = !1,
                            t._checkSessionIFrame.start(e.session_state),
                            e.sid === t._sid ? i.Log.debug("SessionMonitor._callback: Same sub still logged in at OP, restarting check session iframe; session_state:", e.session_state) : (i.Log.debug("SessionMonitor._callback: Same sub still logged in at OP, session state has changed, restarting check session iframe; session_state:", e.session_state),
                            t._userManager.events._raiseUserSessionChanged())) : i.Log.debug("SessionMonitor._callback: Different subject signed into OP:", e.sub) : i.Log.debug("SessionMonitor._callback: Subject no longer signed into OP"),
                            r && (t._sub ? (i.Log.debug("SessionMonitor._callback: SessionMonitor._callback; raising signed out event"),
                            t._userManager.events._raiseUserSignedOut()) : (i.Log.debug("SessionMonitor._callback: SessionMonitor._callback; raising signed in event"),
                            t._userManager.events._raiseUserSignedIn()))
                        }
                        )).catch((function(e) {
                            t._sub && (i.Log.debug("SessionMonitor._callback: Error calling queryCurrentSigninSession; raising signed out event", e.message),
                            t._userManager.events._raiseUserSignedOut())
                        }
                        ))
                    }
                    ,
                    n(t, [{
                        key: "_settings",
                        get: function() {
                            return this._userManager.settings
                        }
                    }, {
                        key: "_metadataService",
                        get: function() {
                            return this._userManager.metadataService
                        }
                    }, {
                        key: "_client_id",
                        get: function() {
                            return this._settings.client_id
                        }
                    }, {
                        key: "_checkSessionInterval",
                        get: function() {
                            return this._settings.checkSessionInterval
                        }
                    }, {
                        key: "_stopCheckSessionOnError",
                        get: function() {
                            return this._settings.stopCheckSessionOnError
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.CheckSessionIFrame = void 0;
                var n = r(0);
                function i(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.CheckSessionIFrame = function() {
                    function t(e, r, n, o) {
                        var s = !(arguments.length > 4 && void 0 !== arguments[4]) || arguments[4];
                        i(this, t),
                        this._callback = e,
                        this._client_id = r,
                        this._url = n,
                        this._interval = o || 2e3,
                        this._stopOnError = s;
                        var a = n.indexOf("/", n.indexOf("//") + 2);
                        this._frame_origin = n.substr(0, a),
                        this._frame = window.document.createElement("iframe"),
                        this._frame.style.visibility = "hidden",
                        this._frame.style.position = "absolute",
                        this._frame.style.display = "none",
                        this._frame.width = 0,
                        this._frame.height = 0,
                        this._frame.src = n
                    }
                    return t.prototype.load = function() {
                        var t = this;
                        return new Promise((function(e) {
                            t._frame.onload = function() {
                                e()
                            }
                            ,
                            window.document.body.appendChild(t._frame),
                            t._boundMessageEvent = t._message.bind(t),
                            window.addEventListener("message", t._boundMessageEvent, !1)
                        }
                        ))
                    }
                    ,
                    t.prototype._message = function(t) {
                        t.origin === this._frame_origin && t.source === this._frame.contentWindow && ("error" === t.data ? (n.Log.error("CheckSessionIFrame: error message from check session op iframe"),
                        this._stopOnError && this.stop()) : "changed" === t.data ? (n.Log.debug("CheckSessionIFrame: changed message from check session op iframe"),
                        this.stop(),
                        this._callback()) : n.Log.debug("CheckSessionIFrame: " + t.data + " message from check session op iframe"))
                    }
                    ,
                    t.prototype.start = function(t) {
                        var e = this;
                        if (this._session_state !== t) {
                            n.Log.debug("CheckSessionIFrame.start"),
                            this.stop(),
                            this._session_state = t;
                            var r = function() {
                                e._frame.contentWindow.postMessage(e._client_id + " " + e._session_state, e._frame_origin)
                            };
                            r(),
                            this._timer = window.setInterval(r, this._interval)
                        }
                    }
                    ,
                    t.prototype.stop = function() {
                        this._session_state = null,
                        this._timer && (n.Log.debug("CheckSessionIFrame.stop"),
                        window.clearInterval(this._timer),
                        this._timer = null)
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.TokenRevocationClient = void 0;
                var n = r(0)
                  , i = r(2)
                  , o = r(1);
                function s(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                var a = "access_token"
                  , u = "refresh_token";
                e.TokenRevocationClient = function() {
                    function t(e) {
                        var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : o.Global.XMLHttpRequest
                          , a = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : i.MetadataService;
                        if (s(this, t),
                        !e)
                            throw n.Log.error("TokenRevocationClient.ctor: No settings provided"),
                            new Error("No settings provided.");
                        this._settings = e,
                        this._XMLHttpRequestCtor = r,
                        this._metadataService = new a(this._settings)
                    }
                    return t.prototype.revoke = function(t, e) {
                        var r = this
                          , i = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : "access_token";
                        if (!t)
                            throw n.Log.error("TokenRevocationClient.revoke: No token provided"),
                            new Error("No token provided.");
                        if (i !== a && i != u)
                            throw n.Log.error("TokenRevocationClient.revoke: Invalid token type"),
                            new Error("Invalid token type.");
                        return this._metadataService.getRevocationEndpoint().then((function(o) {
                            if (o) {
                                n.Log.debug("TokenRevocationClient.revoke: Revoking " + i);
                                var s = r._settings.client_id
                                  , a = r._settings.client_secret;
                                return r._revoke(o, s, a, t, i)
                            }
                            if (e)
                                throw n.Log.error("TokenRevocationClient.revoke: Revocation not supported"),
                                new Error("Revocation not supported")
                        }
                        ))
                    }
                    ,
                    t.prototype._revoke = function(t, e, r, i, o) {
                        var s = this;
                        return new Promise((function(a, u) {
                            var c = new s._XMLHttpRequestCtor;
                            c.open("POST", t),
                            c.onload = function() {
                                n.Log.debug("TokenRevocationClient.revoke: HTTP response received, status", c.status),
                                200 === c.status ? a() : u(Error(c.statusText + " (" + c.status + ")"))
                            }
                            ,
                            c.onerror = function() {
                                n.Log.debug("TokenRevocationClient.revoke: Network Error."),
                                u("Network Error")
                            }
                            ;
                            var h = "client_id=" + encodeURIComponent(e);
                            r && (h += "&client_secret=" + encodeURIComponent(r)),
                            h += "&token_type_hint=" + encodeURIComponent(o),
                            h += "&token=" + encodeURIComponent(i),
                            c.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"),
                            c.send(h)
                        }
                        ))
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.CordovaPopupWindow = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0);
                e.CordovaPopupWindow = function() {
                    function t(e) {
                        var r = this;
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t),
                        this._promise = new Promise((function(t, e) {
                            r._resolve = t,
                            r._reject = e
                        }
                        )),
                        this.features = e.popupWindowFeatures || "location=no,toolbar=no,zoom=no",
                        this.target = e.popupWindowTarget || "_blank",
                        this.redirect_uri = e.startUrl,
                        i.Log.debug("CordovaPopupWindow.ctor: redirect_uri: " + this.redirect_uri)
                    }
                    return t.prototype._isInAppBrowserInstalled = function(t) {
                        return ["cordova-plugin-inappbrowser", "cordova-plugin-inappbrowser.inappbrowser", "org.apache.cordova.inappbrowser"].some((function(e) {
                            return t.hasOwnProperty(e)
                        }
                        ))
                    }
                    ,
                    t.prototype.navigate = function(t) {
                        if (t && t.url) {
                            if (!window.cordova)
                                return this._error("cordova is undefined");
                            var e = window.cordova.require("cordova/plugin_list").metadata;
                            if (!1 === this._isInAppBrowserInstalled(e))
                                return this._error("InAppBrowser plugin not found");
                            this._popup = cordova.InAppBrowser.open(t.url, this.target, this.features),
                            this._popup ? (i.Log.debug("CordovaPopupWindow.navigate: popup successfully created"),
                            this._exitCallbackEvent = this._exitCallback.bind(this),
                            this._loadStartCallbackEvent = this._loadStartCallback.bind(this),
                            this._popup.addEventListener("exit", this._exitCallbackEvent, !1),
                            this._popup.addEventListener("loadstart", this._loadStartCallbackEvent, !1)) : this._error("Error opening popup window")
                        } else
                            this._error("No url provided");
                        return this.promise
                    }
                    ,
                    t.prototype._loadStartCallback = function(t) {
                        0 === t.url.indexOf(this.redirect_uri) && this._success({
                            url: t.url
                        })
                    }
                    ,
                    t.prototype._exitCallback = function(t) {
                        this._error(t)
                    }
                    ,
                    t.prototype._success = function(t) {
                        this._cleanup(),
                        i.Log.debug("CordovaPopupWindow: Successful response from cordova popup window"),
                        this._resolve(t)
                    }
                    ,
                    t.prototype._error = function(t) {
                        this._cleanup(),
                        i.Log.error(t),
                        this._reject(new Error(t))
                    }
                    ,
                    t.prototype.close = function() {
                        this._cleanup()
                    }
                    ,
                    t.prototype._cleanup = function() {
                        this._popup && (i.Log.debug("CordovaPopupWindow: cleaning up popup"),
                        this._popup.removeEventListener("exit", this._exitCallbackEvent, !1),
                        this._popup.removeEventListener("loadstart", this._loadStartCallbackEvent, !1),
                        this._popup.close()),
                        this._popup = null
                    }
                    ,
                    n(t, [{
                        key: "promise",
                        get: function() {
                            return this._promise
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                });
                var n = r(0)
                  , i = r(10)
                  , o = r(5)
                  , s = r(6)
                  , a = r(37)
                  , u = r(38)
                  , c = r(16)
                  , h = r(2)
                  , l = r(48)
                  , f = r(49)
                  , g = r(19)
                  , d = r(20)
                  , p = r(18)
                  , v = r(1)
                  , y = r(15)
                  , m = r(50);
                e.default = {
                    Version: m.Version,
                    Log: n.Log,
                    OidcClient: i.OidcClient,
                    OidcClientSettings: o.OidcClientSettings,
                    WebStorageStateStore: s.WebStorageStateStore,
                    InMemoryWebStorage: a.InMemoryWebStorage,
                    UserManager: u.UserManager,
                    AccessTokenEvents: c.AccessTokenEvents,
                    MetadataService: h.MetadataService,
                    CordovaPopupNavigator: l.CordovaPopupNavigator,
                    CordovaIFrameNavigator: f.CordovaIFrameNavigator,
                    CheckSessionIFrame: g.CheckSessionIFrame,
                    TokenRevocationClient: d.TokenRevocationClient,
                    SessionMonitor: p.SessionMonitor,
                    Global: v.Global,
                    User: y.User
                },
                t.exports = e.default
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.ClockService = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t.prototype.getEpochTime = function() {
                        return Promise.resolve(Date.now() / 1e3 | 0)
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.ResponseValidator = void 0;
                var n = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(t) {
                    return typeof t
                }
                : function(t) {
                    return t && "function" == typeof Symbol && t.constructor === Symbol && t !== Symbol.prototype ? "symbol" : typeof t
                }
                  , i = r(0)
                  , o = r(2)
                  , s = r(25)
                  , a = r(11)
                  , u = r(12)
                  , c = r(4);
                function h(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                var l = ["nonce", "at_hash", "iat", "nbf", "exp", "aud", "iss", "c_hash"];
                e.ResponseValidator = function() {
                    function t(e) {
                        var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : o.MetadataService
                          , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : s.UserInfoService
                          , u = arguments.length > 3 && void 0 !== arguments[3] ? arguments[3] : c.JoseUtil
                          , l = arguments.length > 4 && void 0 !== arguments[4] ? arguments[4] : a.TokenClient;
                        if (h(this, t),
                        !e)
                            throw i.Log.error("ResponseValidator.ctor: No settings passed to ResponseValidator"),
                            new Error("settings");
                        this._settings = e,
                        this._metadataService = new r(this._settings),
                        this._userInfoService = new n(this._settings),
                        this._joseUtil = u,
                        this._tokenClient = new l(this._settings)
                    }
                    return t.prototype.validateSigninResponse = function(t, e) {
                        var r = this;
                        return i.Log.debug("ResponseValidator.validateSigninResponse"),
                        this._processSigninParams(t, e).then((function(e) {
                            return i.Log.debug("ResponseValidator.validateSigninResponse: state processed"),
                            r._validateTokens(t, e).then((function(e) {
                                return i.Log.debug("ResponseValidator.validateSigninResponse: tokens validated"),
                                r._processClaims(t, e).then((function(t) {
                                    return i.Log.debug("ResponseValidator.validateSigninResponse: claims processed"),
                                    t
                                }
                                ))
                            }
                            ))
                        }
                        ))
                    }
                    ,
                    t.prototype.validateSignoutResponse = function(t, e) {
                        return t.id !== e.state ? (i.Log.error("ResponseValidator.validateSignoutResponse: State does not match"),
                        Promise.reject(new Error("State does not match"))) : (i.Log.debug("ResponseValidator.validateSignoutResponse: state validated"),
                        e.state = t.data,
                        e.error ? (i.Log.warn("ResponseValidator.validateSignoutResponse: Response was error", e.error),
                        Promise.reject(new u.ErrorResponse(e))) : Promise.resolve(e))
                    }
                    ,
                    t.prototype._processSigninParams = function(t, e) {
                        if (t.id !== e.state)
                            return i.Log.error("ResponseValidator._processSigninParams: State does not match"),
                            Promise.reject(new Error("State does not match"));
                        if (!t.client_id)
                            return i.Log.error("ResponseValidator._processSigninParams: No client_id on state"),
                            Promise.reject(new Error("No client_id on state"));
                        if (!t.authority)
                            return i.Log.error("ResponseValidator._processSigninParams: No authority on state"),
                            Promise.reject(new Error("No authority on state"));
                        if (this._settings.authority) {
                            if (this._settings.authority && this._settings.authority !== t.authority)
                                return i.Log.error("ResponseValidator._processSigninParams: authority mismatch on settings vs. signin state"),
                                Promise.reject(new Error("authority mismatch on settings vs. signin state"))
                        } else
                            this._settings.authority = t.authority;
                        if (this._settings.client_id) {
                            if (this._settings.client_id && this._settings.client_id !== t.client_id)
                                return i.Log.error("ResponseValidator._processSigninParams: client_id mismatch on settings vs. signin state"),
                                Promise.reject(new Error("client_id mismatch on settings vs. signin state"))
                        } else
                            this._settings.client_id = t.client_id;
                        return i.Log.debug("ResponseValidator._processSigninParams: state validated"),
                        e.state = t.data,
                        e.error ? (i.Log.warn("ResponseValidator._processSigninParams: Response was error", e.error),
                        Promise.reject(new u.ErrorResponse(e))) : t.nonce && !e.id_token ? (i.Log.error("ResponseValidator._processSigninParams: Expecting id_token in response"),
                        Promise.reject(new Error("No id_token in response"))) : !t.nonce && e.id_token ? (i.Log.error("ResponseValidator._processSigninParams: Not expecting id_token in response"),
                        Promise.reject(new Error("Unexpected id_token in response"))) : t.code_verifier && !e.code ? (i.Log.error("ResponseValidator._processSigninParams: Expecting code in response"),
                        Promise.reject(new Error("No code in response"))) : !t.code_verifier && e.code ? (i.Log.error("ResponseValidator._processSigninParams: Not expecting code in response"),
                        Promise.reject(new Error("Unexpected code in response"))) : (e.scope || (e.scope = t.scope),
                        Promise.resolve(e))
                    }
                    ,
                    t.prototype._processClaims = function(t, e) {
                        var r = this;
                        if (e.isOpenIdConnect) {
                            if (i.Log.debug("ResponseValidator._processClaims: response is OIDC, processing claims"),
                            e.profile = this._filterProtocolClaims(e.profile),
                            !0 !== t.skipUserInfo && this._settings.loadUserInfo && e.access_token)
                                return i.Log.debug("ResponseValidator._processClaims: loading user info"),
                                this._userInfoService.getClaims(e.access_token).then((function(t) {
                                    return i.Log.debug("ResponseValidator._processClaims: user info claims received from user info endpoint"),
                                    t.sub !== e.profile.sub ? (i.Log.error("ResponseValidator._processClaims: sub from user info endpoint does not match sub in id_token"),
                                    Promise.reject(new Error("sub from user info endpoint does not match sub in id_token"))) : (e.profile = r._mergeClaims(e.profile, t),
                                    i.Log.debug("ResponseValidator._processClaims: user info claims received, updated profile:", e.profile),
                                    e)
                                }
                                ));
                            i.Log.debug("ResponseValidator._processClaims: not loading user info")
                        } else
                            i.Log.debug("ResponseValidator._processClaims: response is not OIDC, not processing claims");
                        return Promise.resolve(e)
                    }
                    ,
                    t.prototype._mergeClaims = function(t, e) {
                        var r = Object.assign({}, t);
                        for (var i in e) {
                            var o = e[i];
                            Array.isArray(o) || (o = [o]);
                            for (var s = 0; s < o.length; s++) {
                                var a = o[s];
                                r[i] ? Array.isArray(r[i]) ? r[i].indexOf(a) < 0 && r[i].push(a) : r[i] !== a && ("object" === (void 0 === a ? "undefined" : n(a)) && this._settings.mergeClaims ? r[i] = this._mergeClaims(r[i], a) : r[i] = [r[i], a]) : r[i] = a
                            }
                        }
                        return r
                    }
                    ,
                    t.prototype._filterProtocolClaims = function(t) {
                        i.Log.debug("ResponseValidator._filterProtocolClaims, incoming claims:", t);
                        var e = Object.assign({}, t);
                        return this._settings._filterProtocolClaims ? (l.forEach((function(t) {
                            delete e[t]
                        }
                        )),
                        i.Log.debug("ResponseValidator._filterProtocolClaims: protocol claims filtered", e)) : i.Log.debug("ResponseValidator._filterProtocolClaims: protocol claims not filtered"),
                        e
                    }
                    ,
                    t.prototype._validateTokens = function(t, e) {
                        return e.code ? (i.Log.debug("ResponseValidator._validateTokens: Validating code"),
                        this._processCode(t, e)) : e.id_token ? e.access_token ? (i.Log.debug("ResponseValidator._validateTokens: Validating id_token and access_token"),
                        this._validateIdTokenAndAccessToken(t, e)) : (i.Log.debug("ResponseValidator._validateTokens: Validating id_token"),
                        this._validateIdToken(t, e)) : (i.Log.debug("ResponseValidator._validateTokens: No code to process or id_token to validate"),
                        Promise.resolve(e))
                    }
                    ,
                    t.prototype._processCode = function(t, e) {
                        var r = this
                          , o = {
                            client_id: t.client_id,
                            client_secret: t.client_secret,
                            code: e.code,
                            redirect_uri: t.redirect_uri,
                            code_verifier: t.code_verifier
                        };
                        return t.extraTokenParams && "object" === n(t.extraTokenParams) && Object.assign(o, t.extraTokenParams),
                        this._tokenClient.exchangeCode(o).then((function(n) {
                            for (var o in n)
                                e[o] = n[o];
                            return e.id_token ? (i.Log.debug("ResponseValidator._processCode: token response successful, processing id_token"),
                            r._validateIdTokenAttributes(t, e)) : (i.Log.debug("ResponseValidator._processCode: token response successful, returning response"),
                            e)
                        }
                        ))
                    }
                    ,
                    t.prototype._validateIdTokenAttributes = function(t, e) {
                        var r = this;
                        return this._metadataService.getIssuer().then((function(n) {
                            var o = t.client_id
                              , s = r._settings.clockSkew;
                            return i.Log.debug("ResponseValidator._validateIdTokenAttributes: Validaing JWT attributes; using clock skew (in seconds) of: ", s),
                            r._settings.getEpochTime().then((function(a) {
                                return r._joseUtil.validateJwtAttributes(e.id_token, n, o, s, a).then((function(r) {
                                    return t.nonce && t.nonce !== r.nonce ? (i.Log.error("ResponseValidator._validateIdTokenAttributes: Invalid nonce in id_token"),
                                    Promise.reject(new Error("Invalid nonce in id_token"))) : r.sub ? (e.profile = r,
                                    e) : (i.Log.error("ResponseValidator._validateIdTokenAttributes: No sub present in id_token"),
                                    Promise.reject(new Error("No sub present in id_token")))
                                }
                                ))
                            }
                            ))
                        }
                        ))
                    }
                    ,
                    t.prototype._validateIdTokenAndAccessToken = function(t, e) {
                        var r = this;
                        return this._validateIdToken(t, e).then((function(t) {
                            return r._validateAccessToken(t)
                        }
                        ))
                    }
                    ,
                    t.prototype._getSigningKeyForJwt = function(t) {
                        var e = this;
                        return this._metadataService.getSigningKeys().then((function(r) {
                            var n = t.header.kid;
                            if (!r)
                                return i.Log.error("ResponseValidator._validateIdToken: No signing keys from metadata"),
                                Promise.reject(new Error("No signing keys from metadata"));
                            i.Log.debug("ResponseValidator._validateIdToken: Received signing keys");
                            var o = void 0;
                            if (n)
                                o = r.filter((function(t) {
                                    return t.kid === n
                                }
                                ))[0];
                            else {
                                if ((r = e._filterByAlg(r, t.header.alg)).length > 1)
                                    return i.Log.error("ResponseValidator._validateIdToken: No kid found in id_token and more than one key found in metadata"),
                                    Promise.reject(new Error("No kid found in id_token and more than one key found in metadata"));
                                o = r[0]
                            }
                            return Promise.resolve(o)
                        }
                        ))
                    }
                    ,
                    t.prototype._getSigningKeyForJwtWithSingleRetry = function(t) {
                        var e = this;
                        return this._getSigningKeyForJwt(t).then((function(r) {
                            return r ? Promise.resolve(r) : (e._metadataService.resetSigningKeys(),
                            e._getSigningKeyForJwt(t))
                        }
                        ))
                    }
                    ,
                    t.prototype._validateIdToken = function(t, e) {
                        var r = this;
                        if (!t.nonce)
                            return i.Log.error("ResponseValidator._validateIdToken: No nonce on state"),
                            Promise.reject(new Error("No nonce on state"));
                        var n = this._joseUtil.parseJwt(e.id_token);
                        return n && n.header && n.payload ? t.nonce !== n.payload.nonce ? (i.Log.error("ResponseValidator._validateIdToken: Invalid nonce in id_token"),
                        Promise.reject(new Error("Invalid nonce in id_token"))) : this._metadataService.getIssuer().then((function(o) {
                            return i.Log.debug("ResponseValidator._validateIdToken: Received issuer"),
                            r._getSigningKeyForJwtWithSingleRetry(n).then((function(s) {
                                if (!s)
                                    return i.Log.error("ResponseValidator._validateIdToken: No key matching kid or alg found in signing keys"),
                                    Promise.reject(new Error("No key matching kid or alg found in signing keys"));
                                var a = t.client_id
                                  , u = r._settings.clockSkew;
                                return i.Log.debug("ResponseValidator._validateIdToken: Validaing JWT; using clock skew (in seconds) of: ", u),
                                r._joseUtil.validateJwt(e.id_token, s, o, a, u).then((function() {
                                    return i.Log.debug("ResponseValidator._validateIdToken: JWT validation successful"),
                                    n.payload.sub ? (e.profile = n.payload,
                                    e) : (i.Log.error("ResponseValidator._validateIdToken: No sub present in id_token"),
                                    Promise.reject(new Error("No sub present in id_token")))
                                }
                                ))
                            }
                            ))
                        }
                        )) : (i.Log.error("ResponseValidator._validateIdToken: Failed to parse id_token", n),
                        Promise.reject(new Error("Failed to parse id_token")))
                    }
                    ,
                    t.prototype._filterByAlg = function(t, e) {
                        var r = null;
                        if (e.startsWith("RS"))
                            r = "RSA";
                        else if (e.startsWith("PS"))
                            r = "PS";
                        else {
                            if (!e.startsWith("ES"))
                                return i.Log.debug("ResponseValidator._filterByAlg: alg not supported: ", e),
                                [];
                            r = "EC"
                        }
                        return i.Log.debug("ResponseValidator._filterByAlg: Looking for keys that match kty: ", r),
                        t = t.filter((function(t) {
                            return t.kty === r
                        }
                        )),
                        i.Log.debug("ResponseValidator._filterByAlg: Number of keys that match kty: ", r, t.length),
                        t
                    }
                    ,
                    t.prototype._validateAccessToken = function(t) {
                        if (!t.profile)
                            return i.Log.error("ResponseValidator._validateAccessToken: No profile loaded from id_token"),
                            Promise.reject(new Error("No profile loaded from id_token"));
                        if (!t.profile.at_hash)
                            return i.Log.error("ResponseValidator._validateAccessToken: No at_hash in id_token"),
                            Promise.reject(new Error("No at_hash in id_token"));
                        if (!t.id_token)
                            return i.Log.error("ResponseValidator._validateAccessToken: No id_token"),
                            Promise.reject(new Error("No id_token"));
                        var e = this._joseUtil.parseJwt(t.id_token);
                        if (!e || !e.header)
                            return i.Log.error("ResponseValidator._validateAccessToken: Failed to parse id_token", e),
                            Promise.reject(new Error("Failed to parse id_token"));
                        var r = e.header.alg;
                        if (!r || 5 !== r.length)
                            return i.Log.error("ResponseValidator._validateAccessToken: Unsupported alg:", r),
                            Promise.reject(new Error("Unsupported alg: " + r));
                        var n = r.substr(2, 3);
                        if (!n)
                            return i.Log.error("ResponseValidator._validateAccessToken: Unsupported alg:", r, n),
                            Promise.reject(new Error("Unsupported alg: " + r));
                        if (256 !== (n = parseInt(n)) && 384 !== n && 512 !== n)
                            return i.Log.error("ResponseValidator._validateAccessToken: Unsupported alg:", r, n),
                            Promise.reject(new Error("Unsupported alg: " + r));
                        var o = "sha" + n
                          , s = this._joseUtil.hashString(t.access_token, o);
                        if (!s)
                            return i.Log.error("ResponseValidator._validateAccessToken: access_token hash failed:", o),
                            Promise.reject(new Error("Failed to validate at_hash"));
                        var a = s.substr(0, s.length / 2)
                          , u = this._joseUtil.hexToBase64Url(a);
                        return u !== t.profile.at_hash ? (i.Log.error("ResponseValidator._validateAccessToken: Failed to validate at_hash", u, t.profile.at_hash),
                        Promise.reject(new Error("Failed to validate at_hash"))) : (i.Log.debug("ResponseValidator._validateAccessToken: success"),
                        Promise.resolve(t))
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.UserInfoService = void 0;
                var n = r(7)
                  , i = r(2)
                  , o = r(0)
                  , s = r(4);
                function a(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.UserInfoService = function() {
                    function t(e) {
                        var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : n.JsonService
                          , u = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : i.MetadataService
                          , c = arguments.length > 3 && void 0 !== arguments[3] ? arguments[3] : s.JoseUtil;
                        if (a(this, t),
                        !e)
                            throw o.Log.error("UserInfoService.ctor: No settings passed"),
                            new Error("settings");
                        this._settings = e,
                        this._jsonService = new r(void 0,void 0,this._getClaimsFromJwt.bind(this)),
                        this._metadataService = new u(this._settings),
                        this._joseUtil = c
                    }
                    return t.prototype.getClaims = function(t) {
                        var e = this;
                        return t ? this._metadataService.getUserInfoEndpoint().then((function(r) {
                            return o.Log.debug("UserInfoService.getClaims: received userinfo url", r),
                            e._jsonService.getJson(r, t).then((function(t) {
                                return o.Log.debug("UserInfoService.getClaims: claims received", t),
                                t
                            }
                            ))
                        }
                        )) : (o.Log.error("UserInfoService.getClaims: No token passed"),
                        Promise.reject(new Error("A token is required")))
                    }
                    ,
                    t.prototype._getClaimsFromJwt = function t(e) {
                        var r = this;
                        try {
                            var n = this._joseUtil.parseJwt(e.responseText);
                            if (!n || !n.header || !n.payload)
                                return o.Log.error("UserInfoService._getClaimsFromJwt: Failed to parse JWT", n),
                                Promise.reject(new Error("Failed to parse id_token"));
                            var i = n.header.kid
                              , s = void 0;
                            switch (this._settings.userInfoJwtIssuer) {
                            case "OP":
                                s = this._metadataService.getIssuer();
                                break;
                            case "ANY":
                                s = Promise.resolve(n.payload.iss);
                                break;
                            default:
                                s = Promise.resolve(this._settings.userInfoJwtIssuer)
                            }
                            return s.then((function(t) {
                                return o.Log.debug("UserInfoService._getClaimsFromJwt: Received issuer:" + t),
                                r._metadataService.getSigningKeys().then((function(s) {
                                    if (!s)
                                        return o.Log.error("UserInfoService._getClaimsFromJwt: No signing keys from metadata"),
                                        Promise.reject(new Error("No signing keys from metadata"));
                                    o.Log.debug("UserInfoService._getClaimsFromJwt: Received signing keys");
                                    var a = void 0;
                                    if (i)
                                        a = s.filter((function(t) {
                                            return t.kid === i
                                        }
                                        ))[0];
                                    else {
                                        if ((s = r._filterByAlg(s, n.header.alg)).length > 1)
                                            return o.Log.error("UserInfoService._getClaimsFromJwt: No kid found in id_token and more than one key found in metadata"),
                                            Promise.reject(new Error("No kid found in id_token and more than one key found in metadata"));
                                        a = s[0]
                                    }
                                    if (!a)
                                        return o.Log.error("UserInfoService._getClaimsFromJwt: No key matching kid or alg found in signing keys"),
                                        Promise.reject(new Error("No key matching kid or alg found in signing keys"));
                                    var u = r._settings.client_id
                                      , c = r._settings.clockSkew;
                                    return o.Log.debug("UserInfoService._getClaimsFromJwt: Validaing JWT; using clock skew (in seconds) of: ", c),
                                    r._joseUtil.validateJwt(e.responseText, a, t, u, c, void 0, !0).then((function() {
                                        return o.Log.debug("UserInfoService._getClaimsFromJwt: JWT validation successful"),
                                        n.payload
                                    }
                                    ))
                                }
                                ))
                            }
                            ))
                        } catch (t) {
                            return o.Log.error("UserInfoService._getClaimsFromJwt: Error parsing JWT response", t.message),
                            void reject(t)
                        }
                    }
                    ,
                    t.prototype._filterByAlg = function(t, e) {
                        var r = null;
                        if (e.startsWith("RS"))
                            r = "RSA";
                        else if (e.startsWith("PS"))
                            r = "PS";
                        else {
                            if (!e.startsWith("ES"))
                                return o.Log.debug("UserInfoService._filterByAlg: alg not supported: ", e),
                                [];
                            r = "EC"
                        }
                        return o.Log.debug("UserInfoService._filterByAlg: Looking for keys that match kty: ", r),
                        t = t.filter((function(t) {
                            return t.kty === r
                        }
                        )),
                        o.Log.debug("UserInfoService._filterByAlg: Number of keys that match kty: ", r, t.length),
                        t
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.AllowedSigningAlgs = e.b64tohex = e.hextob64u = e.crypto = e.X509 = e.KeyUtil = e.jws = void 0;
                var n = r(27);
                e.jws = n.jws,
                e.KeyUtil = n.KEYUTIL,
                e.X509 = n.X509,
                e.crypto = n.crypto,
                e.hextob64u = n.hextob64u,
                e.b64tohex = n.b64tohex,
                e.AllowedSigningAlgs = ["RS256", "RS384", "RS512", "PS256", "PS384", "PS512", "ES256", "ES384", "ES512"]
            }
            , function(t, e, r) {
                "use strict";
                (function(t) {
                    Object.defineProperty(e, "__esModule", {
                        value: !0
                    });
                    var r = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(t) {
                        return typeof t
                    }
                    : function(t) {
                        return t && "function" == typeof Symbol && t.constructor === Symbol && t !== Symbol.prototype ? "symbol" : typeof t
                    }
                      , n = {
                        userAgent: !1
                    }
                      , i = {};
                    if (void 0 === o)
                        var o = {};
                    o.lang = {
                        extend: function(t, e, r) {
                            if (!e || !t)
                                throw new Error("YAHOO.lang.extend failed, please check that all dependencies are included.");
                            var i = function() {};
                            if (i.prototype = e.prototype,
                            t.prototype = new i,
                            t.prototype.constructor = t,
                            t.superclass = e.prototype,
                            e.prototype.constructor == Object.prototype.constructor && (e.prototype.constructor = e),
                            r) {
                                var o;
                                for (o in r)
                                    t.prototype[o] = r[o];
                                var s = function() {}
                                  , a = ["toString", "valueOf"];
                                try {
                                    /MSIE/.test(n.userAgent) && (s = function(t, e) {
                                        for (o = 0; o < a.length; o += 1) {
                                            var r = a[o]
                                              , n = e[r];
                                            "function" == typeof n && n != Object.prototype[r] && (t[r] = n)
                                        }
                                    }
                                    )
                                } catch (t) {}
                                s(t.prototype, r)
                            }
                        }
                    };
                    var s, a, u, c, h, l, f, g, d, p, v, y = y || (s = Math,
                    u = (a = {}).lib = {},
                    c = u.Base = function() {
                        function t() {}
                        return {
                            extend: function(e) {
                                t.prototype = this;
                                var r = new t;
                                return e && r.mixIn(e),
                                r.hasOwnProperty("init") || (r.init = function() {
                                    r.$super.init.apply(this, arguments)
                                }
                                ),
                                r.init.prototype = r,
                                r.$super = this,
                                r
                            },
                            create: function() {
                                var t = this.extend();
                                return t.init.apply(t, arguments),
                                t
                            },
                            init: function() {},
                            mixIn: function(t) {
                                for (var e in t)
                                    t.hasOwnProperty(e) && (this[e] = t[e]);
                                t.hasOwnProperty("toString") && (this.toString = t.toString)
                            },
                            clone: function() {
                                return this.init.prototype.extend(this)
                            }
                        }
                    }(),
                    h = u.WordArray = c.extend({
                        init: function(t, e) {
                            t = this.words = t || [],
                            this.sigBytes = null != e ? e : 4 * t.length
                        },
                        toString: function(t) {
                            return (t || f).stringify(this)
                        },
                        concat: function(t) {
                            var e = this.words
                              , r = t.words
                              , n = this.sigBytes
                              , i = t.sigBytes;
                            if (this.clamp(),
                            n % 4)
                                for (var o = 0; o < i; o++) {
                                    var s = r[o >>> 2] >>> 24 - o % 4 * 8 & 255;
                                    e[n + o >>> 2] |= s << 24 - (n + o) % 4 * 8
                                }
                            else
                                for (o = 0; o < i; o += 4)
                                    e[n + o >>> 2] = r[o >>> 2];
                            return this.sigBytes += i,
                            this
                        },
                        clamp: function() {
                            var t = this.words
                              , e = this.sigBytes;
                            t[e >>> 2] &= 4294967295 << 32 - e % 4 * 8,
                            t.length = s.ceil(e / 4)
                        },
                        clone: function() {
                            var t = c.clone.call(this);
                            return t.words = this.words.slice(0),
                            t
                        },
                        random: function(t) {
                            for (var e = [], r = 0; r < t; r += 4)
                                e.push(4294967296 * s.random() | 0);
                            return new h.init(e,t)
                        }
                    }),
                    l = a.enc = {},
                    f = l.Hex = {
                        stringify: function(t) {
                            for (var e = t.words, r = t.sigBytes, n = [], i = 0; i < r; i++) {
                                var o = e[i >>> 2] >>> 24 - i % 4 * 8 & 255;
                                n.push((o >>> 4).toString(16)),
                                n.push((15 & o).toString(16))
                            }
                            return n.join("")
                        },
                        parse: function(t) {
                            for (var e = t.length, r = [], n = 0; n < e; n += 2)
                                r[n >>> 3] |= parseInt(t.substr(n, 2), 16) << 24 - n % 8 * 4;
                            return new h.init(r,e / 2)
                        }
                    },
                    g = l.Latin1 = {
                        stringify: function(t) {
                            for (var e = t.words, r = t.sigBytes, n = [], i = 0; i < r; i++) {
                                var o = e[i >>> 2] >>> 24 - i % 4 * 8 & 255;
                                n.push(String.fromCharCode(o))
                            }
                            return n.join("")
                        },
                        parse: function(t) {
                            for (var e = t.length, r = [], n = 0; n < e; n++)
                                r[n >>> 2] |= (255 & t.charCodeAt(n)) << 24 - n % 4 * 8;
                            return new h.init(r,e)
                        }
                    },
                    d = l.Utf8 = {
                        stringify: function(t) {
                            try {
                                return decodeURIComponent(escape(g.stringify(t)))
                            } catch (t) {
                                throw new Error("Malformed UTF-8 data")
                            }
                        },
                        parse: function(t) {
                            return g.parse(unescape(encodeURIComponent(t)))
                        }
                    },
                    p = u.BufferedBlockAlgorithm = c.extend({
                        reset: function() {
                            this._data = new h.init,
                            this._nDataBytes = 0
                        },
                        _append: function(t) {
                            "string" == typeof t && (t = d.parse(t)),
                            this._data.concat(t),
                            this._nDataBytes += t.sigBytes
                        },
                        _process: function(t) {
                            var e = this._data
                              , r = e.words
                              , n = e.sigBytes
                              , i = this.blockSize
                              , o = n / (4 * i)
                              , a = (o = t ? s.ceil(o) : s.max((0 | o) - this._minBufferSize, 0)) * i
                              , u = s.min(4 * a, n);
                            if (a) {
                                for (var c = 0; c < a; c += i)
                                    this._doProcessBlock(r, c);
                                var l = r.splice(0, a);
                                e.sigBytes -= u
                            }
                            return new h.init(l,u)
                        },
                        clone: function() {
                            var t = c.clone.call(this);
                            return t._data = this._data.clone(),
                            t
                        },
                        _minBufferSize: 0
                    }),
                    u.Hasher = p.extend({
                        cfg: c.extend(),
                        init: function(t) {
                            this.cfg = this.cfg.extend(t),
                            this.reset()
                        },
                        reset: function() {
                            p.reset.call(this),
                            this._doReset()
                        },
                        update: function(t) {
                            return this._append(t),
                            this._process(),
                            this
                        },
                        finalize: function(t) {
                            return t && this._append(t),
                            this._doFinalize()
                        },
                        blockSize: 16,
                        _createHelper: function(t) {
                            return function(e, r) {
                                return new t.init(r).finalize(e)
                            }
                        },
                        _createHmacHelper: function(t) {
                            return function(e, r) {
                                return new v.HMAC.init(t,r).finalize(e)
                            }
                        }
                    }),
                    v = a.algo = {},
                    a);
                    !function(t) {
                        var e, r = (e = y).lib, n = r.Base, i = r.WordArray;
                        (e = e.x64 = {}).Word = n.extend({
                            init: function(t, e) {
                                this.high = t,
                                this.low = e
                            }
                        }),
                        e.WordArray = n.extend({
                            init: function(t, e) {
                                t = this.words = t || [],
                                this.sigBytes = null != e ? e : 8 * t.length
                            },
                            toX32: function() {
                                for (var t = this.words, e = t.length, r = [], n = 0; n < e; n++) {
                                    var o = t[n];
                                    r.push(o.high),
                                    r.push(o.low)
                                }
                                return i.create(r, this.sigBytes)
                            },
                            clone: function() {
                                for (var t = n.clone.call(this), e = t.words = this.words.slice(0), r = e.length, i = 0; i < r; i++)
                                    e[i] = e[i].clone();
                                return t
                            }
                        })
                    }(),
                    function() {
                        var t = y
                          , e = t.lib.WordArray;
                        t.enc.Base64 = {
                            stringify: function(t) {
                                var e = t.words
                                  , r = t.sigBytes
                                  , n = this._map;
                                t.clamp(),
                                t = [];
                                for (var i = 0; i < r; i += 3)
                                    for (var o = (e[i >>> 2] >>> 24 - i % 4 * 8 & 255) << 16 | (e[i + 1 >>> 2] >>> 24 - (i + 1) % 4 * 8 & 255) << 8 | e[i + 2 >>> 2] >>> 24 - (i + 2) % 4 * 8 & 255, s = 0; 4 > s && i + .75 * s < r; s++)
                                        t.push(n.charAt(o >>> 6 * (3 - s) & 63));
                                if (e = n.charAt(64))
                                    for (; t.length % 4; )
                                        t.push(e);
                                return t.join("")
                            },
                            parse: function(t) {
                                var r = t.length
                                  , n = this._map;
                                (i = n.charAt(64)) && -1 != (i = t.indexOf(i)) && (r = i);
                                for (var i = [], o = 0, s = 0; s < r; s++)
                                    if (s % 4) {
                                        var a = n.indexOf(t.charAt(s - 1)) << s % 4 * 2
                                          , u = n.indexOf(t.charAt(s)) >>> 6 - s % 4 * 2;
                                        i[o >>> 2] |= (a | u) << 24 - o % 4 * 8,
                                        o++
                                    }
                                return e.create(i, o)
                            },
                            _map: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/="
                        }
                    }(),
                    function(t) {
                        for (var e = y, r = (i = e.lib).WordArray, n = i.Hasher, i = e.algo, o = [], s = [], a = function(t) {
                            return 4294967296 * (t - (0 | t)) | 0
                        }, u = 2, c = 0; 64 > c; ) {
                            var h;
                            t: {
                                h = u;
                                for (var l = t.sqrt(h), f = 2; f <= l; f++)
                                    if (!(h % f)) {
                                        h = !1;
                                        break t
                                    }
                                h = !0
                            }
                            h && (8 > c && (o[c] = a(t.pow(u, .5))),
                            s[c] = a(t.pow(u, 1 / 3)),
                            c++),
                            u++
                        }
                        var g = [];
                        i = i.SHA256 = n.extend({
                            _doReset: function() {
                                this._hash = new r.init(o.slice(0))
                            },
                            _doProcessBlock: function(t, e) {
                                for (var r = this._hash.words, n = r[0], i = r[1], o = r[2], a = r[3], u = r[4], c = r[5], h = r[6], l = r[7], f = 0; 64 > f; f++) {
                                    if (16 > f)
                                        g[f] = 0 | t[e + f];
                                    else {
                                        var d = g[f - 15]
                                          , p = g[f - 2];
                                        g[f] = ((d << 25 | d >>> 7) ^ (d << 14 | d >>> 18) ^ d >>> 3) + g[f - 7] + ((p << 15 | p >>> 17) ^ (p << 13 | p >>> 19) ^ p >>> 10) + g[f - 16]
                                    }
                                    d = l + ((u << 26 | u >>> 6) ^ (u << 21 | u >>> 11) ^ (u << 7 | u >>> 25)) + (u & c ^ ~u & h) + s[f] + g[f],
                                    p = ((n << 30 | n >>> 2) ^ (n << 19 | n >>> 13) ^ (n << 10 | n >>> 22)) + (n & i ^ n & o ^ i & o),
                                    l = h,
                                    h = c,
                                    c = u,
                                    u = a + d | 0,
                                    a = o,
                                    o = i,
                                    i = n,
                                    n = d + p | 0
                                }
                                r[0] = r[0] + n | 0,
                                r[1] = r[1] + i | 0,
                                r[2] = r[2] + o | 0,
                                r[3] = r[3] + a | 0,
                                r[4] = r[4] + u | 0,
                                r[5] = r[5] + c | 0,
                                r[6] = r[6] + h | 0,
                                r[7] = r[7] + l | 0
                            },
                            _doFinalize: function() {
                                var e = this._data
                                  , r = e.words
                                  , n = 8 * this._nDataBytes
                                  , i = 8 * e.sigBytes;
                                return r[i >>> 5] |= 128 << 24 - i % 32,
                                r[14 + (i + 64 >>> 9 << 4)] = t.floor(n / 4294967296),
                                r[15 + (i + 64 >>> 9 << 4)] = n,
                                e.sigBytes = 4 * r.length,
                                this._process(),
                                this._hash
                            },
                            clone: function() {
                                var t = n.clone.call(this);
                                return t._hash = this._hash.clone(),
                                t
                            }
                        }),
                        e.SHA256 = n._createHelper(i),
                        e.HmacSHA256 = n._createHmacHelper(i)
                    }(Math),
                    function() {
                        function t() {
                            return n.create.apply(n, arguments)
                        }
                        for (var e = y, r = e.lib.Hasher, n = (o = e.x64).Word, i = o.WordArray, o = e.algo, s = [t(1116352408, 3609767458), t(1899447441, 602891725), t(3049323471, 3964484399), t(3921009573, 2173295548), t(961987163, 4081628472), t(1508970993, 3053834265), t(2453635748, 2937671579), t(2870763221, 3664609560), t(3624381080, 2734883394), t(310598401, 1164996542), t(607225278, 1323610764), t(1426881987, 3590304994), t(1925078388, 4068182383), t(2162078206, 991336113), t(2614888103, 633803317), t(3248222580, 3479774868), t(3835390401, 2666613458), t(4022224774, 944711139), t(264347078, 2341262773), t(604807628, 2007800933), t(770255983, 1495990901), t(1249150122, 1856431235), t(1555081692, 3175218132), t(1996064986, 2198950837), t(2554220882, 3999719339), t(2821834349, 766784016), t(2952996808, 2566594879), t(3210313671, 3203337956), t(3336571891, 1034457026), t(3584528711, 2466948901), t(113926993, 3758326383), t(338241895, 168717936), t(666307205, 1188179964), t(773529912, 1546045734), t(1294757372, 1522805485), t(1396182291, 2643833823), t(1695183700, 2343527390), t(1986661051, 1014477480), t(2177026350, 1206759142), t(2456956037, 344077627), t(2730485921, 1290863460), t(2820302411, 3158454273), t(3259730800, 3505952657), t(3345764771, 106217008), t(3516065817, 3606008344), t(3600352804, 1432725776), t(4094571909, 1467031594), t(275423344, 851169720), t(430227734, 3100823752), t(506948616, 1363258195), t(659060556, 3750685593), t(883997877, 3785050280), t(958139571, 3318307427), t(1322822218, 3812723403), t(1537002063, 2003034995), t(1747873779, 3602036899), t(1955562222, 1575990012), t(2024104815, 1125592928), t(2227730452, 2716904306), t(2361852424, 442776044), t(2428436474, 593698344), t(2756734187, 3733110249), t(3204031479, 2999351573), t(3329325298, 3815920427), t(3391569614, 3928383900), t(3515267271, 566280711), t(3940187606, 3454069534), t(4118630271, 4000239992), t(116418474, 1914138554), t(174292421, 2731055270), t(289380356, 3203993006), t(460393269, 320620315), t(685471733, 587496836), t(852142971, 1086792851), t(1017036298, 365543100), t(1126000580, 2618297676), t(1288033470, 3409855158), t(1501505948, 4234509866), t(1607167915, 987167468), t(1816402316, 1246189591)], a = [], u = 0; 80 > u; u++)
                            a[u] = t();
                        o = o.SHA512 = r.extend({
                            _doReset: function() {
                                this._hash = new i.init([new n.init(1779033703,4089235720), new n.init(3144134277,2227873595), new n.init(1013904242,4271175723), new n.init(2773480762,1595750129), new n.init(1359893119,2917565137), new n.init(2600822924,725511199), new n.init(528734635,4215389547), new n.init(1541459225,327033209)])
                            },
                            _doProcessBlock: function(t, e) {
                                for (var r = (l = this._hash.words)[0], n = l[1], i = l[2], o = l[3], u = l[4], c = l[5], h = l[6], l = l[7], f = r.high, g = r.low, d = n.high, p = n.low, v = i.high, y = i.low, m = o.high, _ = o.low, S = u.high, w = u.low, b = c.high, F = c.low, E = h.high, x = h.low, A = l.high, k = l.low, P = f, C = g, T = d, R = p, I = v, D = y, L = m, N = _, U = S, O = w, B = b, M = F, j = E, H = x, K = A, V = k, q = 0; 80 > q; q++) {
                                    var J = a[q];
                                    if (16 > q)
                                        var W = J.high = 0 | t[e + 2 * q]
                                          , z = J.low = 0 | t[e + 2 * q + 1];
                                    else {
                                        W = ((z = (W = a[q - 15]).high) >>> 1 | (Y = W.low) << 31) ^ (z >>> 8 | Y << 24) ^ z >>> 7;
                                        var Y = (Y >>> 1 | z << 31) ^ (Y >>> 8 | z << 24) ^ (Y >>> 7 | z << 25)
                                          , G = ((z = (G = a[q - 2]).high) >>> 19 | (X = G.low) << 13) ^ (z << 3 | X >>> 29) ^ z >>> 6
                                          , X = (X >>> 19 | z << 13) ^ (X << 3 | z >>> 29) ^ (X >>> 6 | z << 26)
                                          , $ = (z = a[q - 7]).high
                                          , Q = (Z = a[q - 16]).high
                                          , Z = Z.low;
                                        W = (W = (W = W + $ + ((z = Y + z.low) >>> 0 < Y >>> 0 ? 1 : 0)) + G + ((z += X) >>> 0 < X >>> 0 ? 1 : 0)) + Q + ((z += Z) >>> 0 < Z >>> 0 ? 1 : 0),
                                        J.high = W,
                                        J.low = z
                                    }
                                    $ = U & B ^ ~U & j,
                                    Z = O & M ^ ~O & H,
                                    J = P & T ^ P & I ^ T & I;
                                    var tt = C & R ^ C & D ^ R & D
                                      , et = (Y = (P >>> 28 | C << 4) ^ (P << 30 | C >>> 2) ^ (P << 25 | C >>> 7),
                                    G = (C >>> 28 | P << 4) ^ (C << 30 | P >>> 2) ^ (C << 25 | P >>> 7),
                                    (X = s[q]).high)
                                      , rt = X.low;
                                    Q = K + ((U >>> 14 | O << 18) ^ (U >>> 18 | O << 14) ^ (U << 23 | O >>> 9)) + ((X = V + ((O >>> 14 | U << 18) ^ (O >>> 18 | U << 14) ^ (O << 23 | U >>> 9))) >>> 0 < V >>> 0 ? 1 : 0),
                                    K = j,
                                    V = H,
                                    j = B,
                                    H = M,
                                    B = U,
                                    M = O,
                                    U = L + (Q = (Q = (Q = Q + $ + ((X += Z) >>> 0 < Z >>> 0 ? 1 : 0)) + et + ((X += rt) >>> 0 < rt >>> 0 ? 1 : 0)) + W + ((X += z) >>> 0 < z >>> 0 ? 1 : 0)) + ((O = N + X | 0) >>> 0 < N >>> 0 ? 1 : 0) | 0,
                                    L = I,
                                    N = D,
                                    I = T,
                                    D = R,
                                    T = P,
                                    R = C,
                                    P = Q + (J = Y + J + ((z = G + tt) >>> 0 < G >>> 0 ? 1 : 0)) + ((C = X + z | 0) >>> 0 < X >>> 0 ? 1 : 0) | 0
                                }
                                g = r.low = g + C,
                                r.high = f + P + (g >>> 0 < C >>> 0 ? 1 : 0),
                                p = n.low = p + R,
                                n.high = d + T + (p >>> 0 < R >>> 0 ? 1 : 0),
                                y = i.low = y + D,
                                i.high = v + I + (y >>> 0 < D >>> 0 ? 1 : 0),
                                _ = o.low = _ + N,
                                o.high = m + L + (_ >>> 0 < N >>> 0 ? 1 : 0),
                                w = u.low = w + O,
                                u.high = S + U + (w >>> 0 < O >>> 0 ? 1 : 0),
                                F = c.low = F + M,
                                c.high = b + B + (F >>> 0 < M >>> 0 ? 1 : 0),
                                x = h.low = x + H,
                                h.high = E + j + (x >>> 0 < H >>> 0 ? 1 : 0),
                                k = l.low = k + V,
                                l.high = A + K + (k >>> 0 < V >>> 0 ? 1 : 0)
                            },
                            _doFinalize: function() {
                                var t = this._data
                                  , e = t.words
                                  , r = 8 * this._nDataBytes
                                  , n = 8 * t.sigBytes;
                                return e[n >>> 5] |= 128 << 24 - n % 32,
                                e[30 + (n + 128 >>> 10 << 5)] = Math.floor(r / 4294967296),
                                e[31 + (n + 128 >>> 10 << 5)] = r,
                                t.sigBytes = 4 * e.length,
                                this._process(),
                                this._hash.toX32()
                            },
                            clone: function() {
                                var t = r.clone.call(this);
                                return t._hash = this._hash.clone(),
                                t
                            },
                            blockSize: 32
                        }),
                        e.SHA512 = r._createHelper(o),
                        e.HmacSHA512 = r._createHmacHelper(o)
                    }(),
                    function() {
                        var t = y
                          , e = (i = t.x64).Word
                          , r = i.WordArray
                          , n = (i = t.algo).SHA512
                          , i = i.SHA384 = n.extend({
                            _doReset: function() {
                                this._hash = new r.init([new e.init(3418070365,3238371032), new e.init(1654270250,914150663), new e.init(2438529370,812702999), new e.init(355462360,4144912697), new e.init(1731405415,4290775857), new e.init(2394180231,1750603025), new e.init(3675008525,1694076839), new e.init(1203062813,3204075428)])
                            },
                            _doFinalize: function() {
                                var t = n._doFinalize.call(this);
                                return t.sigBytes -= 16,
                                t
                            }
                        });
                        t.SHA384 = n._createHelper(i),
                        t.HmacSHA384 = n._createHmacHelper(i)
                    }();
                    var m, _ = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
                    function S(t) {
                        var e, r, n = "";
                        for (e = 0; e + 3 <= t.length; e += 3)
                            r = parseInt(t.substring(e, e + 3), 16),
                            n += _.charAt(r >> 6) + _.charAt(63 & r);
                        for (e + 1 == t.length ? (r = parseInt(t.substring(e, e + 1), 16),
                        n += _.charAt(r << 2)) : e + 2 == t.length && (r = parseInt(t.substring(e, e + 2), 16),
                        n += _.charAt(r >> 2) + _.charAt((3 & r) << 4)); (3 & n.length) > 0; )
                            n += "=";
                        return n
                    }
                    function w(t) {
                        var e, r, n, i = "", o = 0;
                        for (e = 0; e < t.length && "=" != t.charAt(e); ++e)
                            (n = _.indexOf(t.charAt(e))) < 0 || (0 == o ? (i += P(n >> 2),
                            r = 3 & n,
                            o = 1) : 1 == o ? (i += P(r << 2 | n >> 4),
                            r = 15 & n,
                            o = 2) : 2 == o ? (i += P(r),
                            i += P(n >> 2),
                            r = 3 & n,
                            o = 3) : (i += P(r << 2 | n >> 4),
                            i += P(15 & n),
                            o = 0));
                        return 1 == o && (i += P(r << 2)),
                        i
                    }
                    function b(t) {
                        var e, r = w(t), n = new Array;
                        for (e = 0; 2 * e < r.length; ++e)
                            n[e] = parseInt(r.substring(2 * e, 2 * e + 2), 16);
                        return n
                    }
                    function F(t, e, r) {
                        null != t && ("number" == typeof t ? this.fromNumber(t, e, r) : null == e && "string" != typeof t ? this.fromString(t, 256) : this.fromString(t, e))
                    }
                    function E() {
                        return new F(null)
                    }
                    "Microsoft Internet Explorer" == n.appName ? (F.prototype.am = function(t, e, r, n, i, o) {
                        for (var s = 32767 & e, a = e >> 15; --o >= 0; ) {
                            var u = 32767 & this[t]
                              , c = this[t++] >> 15
                              , h = a * u + c * s;
                            i = ((u = s * u + ((32767 & h) << 15) + r[n] + (1073741823 & i)) >>> 30) + (h >>> 15) + a * c + (i >>> 30),
                            r[n++] = 1073741823 & u
                        }
                        return i
                    }
                    ,
                    m = 30) : "Netscape" != n.appName ? (F.prototype.am = function(t, e, r, n, i, o) {
                        for (; --o >= 0; ) {
                            var s = e * this[t++] + r[n] + i;
                            i = Math.floor(s / 67108864),
                            r[n++] = 67108863 & s
                        }
                        return i
                    }
                    ,
                    m = 26) : (F.prototype.am = function(t, e, r, n, i, o) {
                        for (var s = 16383 & e, a = e >> 14; --o >= 0; ) {
                            var u = 16383 & this[t]
                              , c = this[t++] >> 14
                              , h = a * u + c * s;
                            i = ((u = s * u + ((16383 & h) << 14) + r[n] + i) >> 28) + (h >> 14) + a * c,
                            r[n++] = 268435455 & u
                        }
                        return i
                    }
                    ,
                    m = 28),
                    F.prototype.DB = m,
                    F.prototype.DM = (1 << m) - 1,
                    F.prototype.DV = 1 << m,
                    F.prototype.FV = Math.pow(2, 52),
                    F.prototype.F1 = 52 - m,
                    F.prototype.F2 = 2 * m - 52;
                    var x, A, k = new Array;
                    for (x = "0".charCodeAt(0),
                    A = 0; A <= 9; ++A)
                        k[x++] = A;
                    for (x = "a".charCodeAt(0),
                    A = 10; A < 36; ++A)
                        k[x++] = A;
                    for (x = "A".charCodeAt(0),
                    A = 10; A < 36; ++A)
                        k[x++] = A;
                    function P(t) {
                        return "0123456789abcdefghijklmnopqrstuvwxyz".charAt(t)
                    }
                    function C(t, e) {
                        var r = k[t.charCodeAt(e)];
                        return null == r ? -1 : r
                    }
                    function T(t) {
                        var e = E();
                        return e.fromInt(t),
                        e
                    }
                    function R(t) {
                        var e, r = 1;
                        return 0 != (e = t >>> 16) && (t = e,
                        r += 16),
                        0 != (e = t >> 8) && (t = e,
                        r += 8),
                        0 != (e = t >> 4) && (t = e,
                        r += 4),
                        0 != (e = t >> 2) && (t = e,
                        r += 2),
                        0 != (e = t >> 1) && (t = e,
                        r += 1),
                        r
                    }
                    function I(t) {
                        this.m = t
                    }
                    function D(t) {
                        this.m = t,
                        this.mp = t.invDigit(),
                        this.mpl = 32767 & this.mp,
                        this.mph = this.mp >> 15,
                        this.um = (1 << t.DB - 15) - 1,
                        this.mt2 = 2 * t.t
                    }
                    function L(t, e) {
                        return t & e
                    }
                    function N(t, e) {
                        return t | e
                    }
                    function U(t, e) {
                        return t ^ e
                    }
                    function O(t, e) {
                        return t & ~e
                    }
                    function B(t) {
                        if (0 == t)
                            return -1;
                        var e = 0;
                        return 0 == (65535 & t) && (t >>= 16,
                        e += 16),
                        0 == (255 & t) && (t >>= 8,
                        e += 8),
                        0 == (15 & t) && (t >>= 4,
                        e += 4),
                        0 == (3 & t) && (t >>= 2,
                        e += 2),
                        0 == (1 & t) && ++e,
                        e
                    }
                    function M(t) {
                        for (var e = 0; 0 != t; )
                            t &= t - 1,
                            ++e;
                        return e
                    }
                    function j() {}
                    function H(t) {
                        return t
                    }
                    function K(t) {
                        this.r2 = E(),
                        this.q3 = E(),
                        F.ONE.dlShiftTo(2 * t.t, this.r2),
                        this.mu = this.r2.divide(t),
                        this.m = t
                    }
                    I.prototype.convert = function(t) {
                        return t.s < 0 || t.compareTo(this.m) >= 0 ? t.mod(this.m) : t
                    }
                    ,
                    I.prototype.revert = function(t) {
                        return t
                    }
                    ,
                    I.prototype.reduce = function(t) {
                        t.divRemTo(this.m, null, t)
                    }
                    ,
                    I.prototype.mulTo = function(t, e, r) {
                        t.multiplyTo(e, r),
                        this.reduce(r)
                    }
                    ,
                    I.prototype.sqrTo = function(t, e) {
                        t.squareTo(e),
                        this.reduce(e)
                    }
                    ,
                    D.prototype.convert = function(t) {
                        var e = E();
                        return t.abs().dlShiftTo(this.m.t, e),
                        e.divRemTo(this.m, null, e),
                        t.s < 0 && e.compareTo(F.ZERO) > 0 && this.m.subTo(e, e),
                        e
                    }
                    ,
                    D.prototype.revert = function(t) {
                        var e = E();
                        return t.copyTo(e),
                        this.reduce(e),
                        e
                    }
                    ,
                    D.prototype.reduce = function(t) {
                        for (; t.t <= this.mt2; )
                            t[t.t++] = 0;
                        for (var e = 0; e < this.m.t; ++e) {
                            var r = 32767 & t[e]
                              , n = r * this.mpl + ((r * this.mph + (t[e] >> 15) * this.mpl & this.um) << 15) & t.DM;
                            for (t[r = e + this.m.t] += this.m.am(0, n, t, e, 0, this.m.t); t[r] >= t.DV; )
                                t[r] -= t.DV,
                                t[++r]++
                        }
                        t.clamp(),
                        t.drShiftTo(this.m.t, t),
                        t.compareTo(this.m) >= 0 && t.subTo(this.m, t)
                    }
                    ,
                    D.prototype.mulTo = function(t, e, r) {
                        t.multiplyTo(e, r),
                        this.reduce(r)
                    }
                    ,
                    D.prototype.sqrTo = function(t, e) {
                        t.squareTo(e),
                        this.reduce(e)
                    }
                    ,
                    F.prototype.copyTo = function(t) {
                        for (var e = this.t - 1; e >= 0; --e)
                            t[e] = this[e];
                        t.t = this.t,
                        t.s = this.s
                    }
                    ,
                    F.prototype.fromInt = function(t) {
                        this.t = 1,
                        this.s = t < 0 ? -1 : 0,
                        t > 0 ? this[0] = t : t < -1 ? this[0] = t + this.DV : this.t = 0
                    }
                    ,
                    F.prototype.fromString = function(t, e) {
                        var r;
                        if (16 == e)
                            r = 4;
                        else if (8 == e)
                            r = 3;
                        else if (256 == e)
                            r = 8;
                        else if (2 == e)
                            r = 1;
                        else if (32 == e)
                            r = 5;
                        else {
                            if (4 != e)
                                return void this.fromRadix(t, e);
                            r = 2
                        }
                        this.t = 0,
                        this.s = 0;
                        for (var n = t.length, i = !1, o = 0; --n >= 0; ) {
                            var s = 8 == r ? 255 & t[n] : C(t, n);
                            s < 0 ? "-" == t.charAt(n) && (i = !0) : (i = !1,
                            0 == o ? this[this.t++] = s : o + r > this.DB ? (this[this.t - 1] |= (s & (1 << this.DB - o) - 1) << o,
                            this[this.t++] = s >> this.DB - o) : this[this.t - 1] |= s << o,
                            (o += r) >= this.DB && (o -= this.DB))
                        }
                        8 == r && 0 != (128 & t[0]) && (this.s = -1,
                        o > 0 && (this[this.t - 1] |= (1 << this.DB - o) - 1 << o)),
                        this.clamp(),
                        i && F.ZERO.subTo(this, this)
                    }
                    ,
                    F.prototype.clamp = function() {
                        for (var t = this.s & this.DM; this.t > 0 && this[this.t - 1] == t; )
                            --this.t
                    }
                    ,
                    F.prototype.dlShiftTo = function(t, e) {
                        var r;
                        for (r = this.t - 1; r >= 0; --r)
                            e[r + t] = this[r];
                        for (r = t - 1; r >= 0; --r)
                            e[r] = 0;
                        e.t = this.t + t,
                        e.s = this.s
                    }
                    ,
                    F.prototype.drShiftTo = function(t, e) {
                        for (var r = t; r < this.t; ++r)
                            e[r - t] = this[r];
                        e.t = Math.max(this.t - t, 0),
                        e.s = this.s
                    }
                    ,
                    F.prototype.lShiftTo = function(t, e) {
                        var r, n = t % this.DB, i = this.DB - n, o = (1 << i) - 1, s = Math.floor(t / this.DB), a = this.s << n & this.DM;
                        for (r = this.t - 1; r >= 0; --r)
                            e[r + s + 1] = this[r] >> i | a,
                            a = (this[r] & o) << n;
                        for (r = s - 1; r >= 0; --r)
                            e[r] = 0;
                        e[s] = a,
                        e.t = this.t + s + 1,
                        e.s = this.s,
                        e.clamp()
                    }
                    ,
                    F.prototype.rShiftTo = function(t, e) {
                        e.s = this.s;
                        var r = Math.floor(t / this.DB);
                        if (r >= this.t)
                            e.t = 0;
                        else {
                            var n = t % this.DB
                              , i = this.DB - n
                              , o = (1 << n) - 1;
                            e[0] = this[r] >> n;
                            for (var s = r + 1; s < this.t; ++s)
                                e[s - r - 1] |= (this[s] & o) << i,
                                e[s - r] = this[s] >> n;
                            n > 0 && (e[this.t - r - 1] |= (this.s & o) << i),
                            e.t = this.t - r,
                            e.clamp()
                        }
                    }
                    ,
                    F.prototype.subTo = function(t, e) {
                        for (var r = 0, n = 0, i = Math.min(t.t, this.t); r < i; )
                            n += this[r] - t[r],
                            e[r++] = n & this.DM,
                            n >>= this.DB;
                        if (t.t < this.t) {
                            for (n -= t.s; r < this.t; )
                                n += this[r],
                                e[r++] = n & this.DM,
                                n >>= this.DB;
                            n += this.s
                        } else {
                            for (n += this.s; r < t.t; )
                                n -= t[r],
                                e[r++] = n & this.DM,
                                n >>= this.DB;
                            n -= t.s
                        }
                        e.s = n < 0 ? -1 : 0,
                        n < -1 ? e[r++] = this.DV + n : n > 0 && (e[r++] = n),
                        e.t = r,
                        e.clamp()
                    }
                    ,
                    F.prototype.multiplyTo = function(t, e) {
                        var r = this.abs()
                          , n = t.abs()
                          , i = r.t;
                        for (e.t = i + n.t; --i >= 0; )
                            e[i] = 0;
                        for (i = 0; i < n.t; ++i)
                            e[i + r.t] = r.am(0, n[i], e, i, 0, r.t);
                        e.s = 0,
                        e.clamp(),
                        this.s != t.s && F.ZERO.subTo(e, e)
                    }
                    ,
                    F.prototype.squareTo = function(t) {
                        for (var e = this.abs(), r = t.t = 2 * e.t; --r >= 0; )
                            t[r] = 0;
                        for (r = 0; r < e.t - 1; ++r) {
                            var n = e.am(r, e[r], t, 2 * r, 0, 1);
                            (t[r + e.t] += e.am(r + 1, 2 * e[r], t, 2 * r + 1, n, e.t - r - 1)) >= e.DV && (t[r + e.t] -= e.DV,
                            t[r + e.t + 1] = 1)
                        }
                        t.t > 0 && (t[t.t - 1] += e.am(r, e[r], t, 2 * r, 0, 1)),
                        t.s = 0,
                        t.clamp()
                    }
                    ,
                    F.prototype.divRemTo = function(t, e, r) {
                        var n = t.abs();
                        if (!(n.t <= 0)) {
                            var i = this.abs();
                            if (i.t < n.t)
                                return null != e && e.fromInt(0),
                                void (null != r && this.copyTo(r));
                            null == r && (r = E());
                            var o = E()
                              , s = this.s
                              , a = t.s
                              , u = this.DB - R(n[n.t - 1]);
                            u > 0 ? (n.lShiftTo(u, o),
                            i.lShiftTo(u, r)) : (n.copyTo(o),
                            i.copyTo(r));
                            var c = o.t
                              , h = o[c - 1];
                            if (0 != h) {
                                var l = h * (1 << this.F1) + (c > 1 ? o[c - 2] >> this.F2 : 0)
                                  , f = this.FV / l
                                  , g = (1 << this.F1) / l
                                  , d = 1 << this.F2
                                  , p = r.t
                                  , v = p - c
                                  , y = null == e ? E() : e;
                                for (o.dlShiftTo(v, y),
                                r.compareTo(y) >= 0 && (r[r.t++] = 1,
                                r.subTo(y, r)),
                                F.ONE.dlShiftTo(c, y),
                                y.subTo(o, o); o.t < c; )
                                    o[o.t++] = 0;
                                for (; --v >= 0; ) {
                                    var m = r[--p] == h ? this.DM : Math.floor(r[p] * f + (r[p - 1] + d) * g);
                                    if ((r[p] += o.am(0, m, r, v, 0, c)) < m)
                                        for (o.dlShiftTo(v, y),
                                        r.subTo(y, r); r[p] < --m; )
                                            r.subTo(y, r)
                                }
                                null != e && (r.drShiftTo(c, e),
                                s != a && F.ZERO.subTo(e, e)),
                                r.t = c,
                                r.clamp(),
                                u > 0 && r.rShiftTo(u, r),
                                s < 0 && F.ZERO.subTo(r, r)
                            }
                        }
                    }
                    ,
                    F.prototype.invDigit = function() {
                        if (this.t < 1)
                            return 0;
                        var t = this[0];
                        if (0 == (1 & t))
                            return 0;
                        var e = 3 & t;
                        return (e = (e = (e = (e = e * (2 - (15 & t) * e) & 15) * (2 - (255 & t) * e) & 255) * (2 - ((65535 & t) * e & 65535)) & 65535) * (2 - t * e % this.DV) % this.DV) > 0 ? this.DV - e : -e
                    }
                    ,
                    F.prototype.isEven = function() {
                        return 0 == (this.t > 0 ? 1 & this[0] : this.s)
                    }
                    ,
                    F.prototype.exp = function(t, e) {
                        if (t > 4294967295 || t < 1)
                            return F.ONE;
                        var r = E()
                          , n = E()
                          , i = e.convert(this)
                          , o = R(t) - 1;
                        for (i.copyTo(r); --o >= 0; )
                            if (e.sqrTo(r, n),
                            (t & 1 << o) > 0)
                                e.mulTo(n, i, r);
                            else {
                                var s = r;
                                r = n,
                                n = s
                            }
                        return e.revert(r)
                    }
                    ,
                    F.prototype.toString = function(t) {
                        if (this.s < 0)
                            return "-" + this.negate().toString(t);
                        var e;
                        if (16 == t)
                            e = 4;
                        else if (8 == t)
                            e = 3;
                        else if (2 == t)
                            e = 1;
                        else if (32 == t)
                            e = 5;
                        else {
                            if (4 != t)
                                return this.toRadix(t);
                            e = 2
                        }
                        var r, n = (1 << e) - 1, i = !1, o = "", s = this.t, a = this.DB - s * this.DB % e;
                        if (s-- > 0)
                            for (a < this.DB && (r = this[s] >> a) > 0 && (i = !0,
                            o = P(r)); s >= 0; )
                                a < e ? (r = (this[s] & (1 << a) - 1) << e - a,
                                r |= this[--s] >> (a += this.DB - e)) : (r = this[s] >> (a -= e) & n,
                                a <= 0 && (a += this.DB,
                                --s)),
                                r > 0 && (i = !0),
                                i && (o += P(r));
                        return i ? o : "0"
                    }
                    ,
                    F.prototype.negate = function() {
                        var t = E();
                        return F.ZERO.subTo(this, t),
                        t
                    }
                    ,
                    F.prototype.abs = function() {
                        return this.s < 0 ? this.negate() : this
                    }
                    ,
                    F.prototype.compareTo = function(t) {
                        var e = this.s - t.s;
                        if (0 != e)
                            return e;
                        var r = this.t;
                        if (0 != (e = r - t.t))
                            return this.s < 0 ? -e : e;
                        for (; --r >= 0; )
                            if (0 != (e = this[r] - t[r]))
                                return e;
                        return 0
                    }
                    ,
                    F.prototype.bitLength = function() {
                        return this.t <= 0 ? 0 : this.DB * (this.t - 1) + R(this[this.t - 1] ^ this.s & this.DM)
                    }
                    ,
                    F.prototype.mod = function(t) {
                        var e = E();
                        return this.abs().divRemTo(t, null, e),
                        this.s < 0 && e.compareTo(F.ZERO) > 0 && t.subTo(e, e),
                        e
                    }
                    ,
                    F.prototype.modPowInt = function(t, e) {
                        var r;
                        return r = t < 256 || e.isEven() ? new I(e) : new D(e),
                        this.exp(t, r)
                    }
                    ,
                    F.ZERO = T(0),
                    F.ONE = T(1),
                    j.prototype.convert = H,
                    j.prototype.revert = H,
                    j.prototype.mulTo = function(t, e, r) {
                        t.multiplyTo(e, r)
                    }
                    ,
                    j.prototype.sqrTo = function(t, e) {
                        t.squareTo(e)
                    }
                    ,
                    K.prototype.convert = function(t) {
                        if (t.s < 0 || t.t > 2 * this.m.t)
                            return t.mod(this.m);
                        if (t.compareTo(this.m) < 0)
                            return t;
                        var e = E();
                        return t.copyTo(e),
                        this.reduce(e),
                        e
                    }
                    ,
                    K.prototype.revert = function(t) {
                        return t
                    }
                    ,
                    K.prototype.reduce = function(t) {
                        for (t.drShiftTo(this.m.t - 1, this.r2),
                        t.t > this.m.t + 1 && (t.t = this.m.t + 1,
                        t.clamp()),
                        this.mu.multiplyUpperTo(this.r2, this.m.t + 1, this.q3),
                        this.m.multiplyLowerTo(this.q3, this.m.t + 1, this.r2); t.compareTo(this.r2) < 0; )
                            t.dAddOffset(1, this.m.t + 1);
                        for (t.subTo(this.r2, t); t.compareTo(this.m) >= 0; )
                            t.subTo(this.m, t)
                    }
                    ,
                    K.prototype.mulTo = function(t, e, r) {
                        t.multiplyTo(e, r),
                        this.reduce(r)
                    }
                    ,
                    K.prototype.sqrTo = function(t, e) {
                        t.squareTo(e),
                        this.reduce(e)
                    }
                    ;
                    var V, q, J, W = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997], z = (1 << 26) / W[W.length - 1];
                    function Y() {
                        this.i = 0,
                        this.j = 0,
                        this.S = new Array
                    }
                    function G() {
                        !function(t) {
                            q[J++] ^= 255 & t,
                            q[J++] ^= t >> 8 & 255,
                            q[J++] ^= t >> 16 & 255,
                            q[J++] ^= t >> 24 & 255,
                            J >= 256 && (J -= 256)
                        }((new Date).getTime())
                    }
                    if (F.prototype.chunkSize = function(t) {
                        return Math.floor(Math.LN2 * this.DB / Math.log(t))
                    }
                    ,
                    F.prototype.toRadix = function(t) {
                        if (null == t && (t = 10),
                        0 == this.signum() || t < 2 || t > 36)
                            return "0";
                        var e = this.chunkSize(t)
                          , r = Math.pow(t, e)
                          , n = T(r)
                          , i = E()
                          , o = E()
                          , s = "";
                        for (this.divRemTo(n, i, o); i.signum() > 0; )
                            s = (r + o.intValue()).toString(t).substr(1) + s,
                            i.divRemTo(n, i, o);
                        return o.intValue().toString(t) + s
                    }
                    ,
                    F.prototype.fromRadix = function(t, e) {
                        this.fromInt(0),
                        null == e && (e = 10);
                        for (var r = this.chunkSize(e), n = Math.pow(e, r), i = !1, o = 0, s = 0, a = 0; a < t.length; ++a) {
                            var u = C(t, a);
                            u < 0 ? "-" == t.charAt(a) && 0 == this.signum() && (i = !0) : (s = e * s + u,
                            ++o >= r && (this.dMultiply(n),
                            this.dAddOffset(s, 0),
                            o = 0,
                            s = 0))
                        }
                        o > 0 && (this.dMultiply(Math.pow(e, o)),
                        this.dAddOffset(s, 0)),
                        i && F.ZERO.subTo(this, this)
                    }
                    ,
                    F.prototype.fromNumber = function(t, e, r) {
                        if ("number" == typeof e)
                            if (t < 2)
                                this.fromInt(1);
                            else
                                for (this.fromNumber(t, r),
                                this.testBit(t - 1) || this.bitwiseTo(F.ONE.shiftLeft(t - 1), N, this),
                                this.isEven() && this.dAddOffset(1, 0); !this.isProbablePrime(e); )
                                    this.dAddOffset(2, 0),
                                    this.bitLength() > t && this.subTo(F.ONE.shiftLeft(t - 1), this);
                        else {
                            var n = new Array
                              , i = 7 & t;
                            n.length = 1 + (t >> 3),
                            e.nextBytes(n),
                            i > 0 ? n[0] &= (1 << i) - 1 : n[0] = 0,
                            this.fromString(n, 256)
                        }
                    }
                    ,
                    F.prototype.bitwiseTo = function(t, e, r) {
                        var n, i, o = Math.min(t.t, this.t);
                        for (n = 0; n < o; ++n)
                            r[n] = e(this[n], t[n]);
                        if (t.t < this.t) {
                            for (i = t.s & this.DM,
                            n = o; n < this.t; ++n)
                                r[n] = e(this[n], i);
                            r.t = this.t
                        } else {
                            for (i = this.s & this.DM,
                            n = o; n < t.t; ++n)
                                r[n] = e(i, t[n]);
                            r.t = t.t
                        }
                        r.s = e(this.s, t.s),
                        r.clamp()
                    }
                    ,
                    F.prototype.changeBit = function(t, e) {
                        var r = F.ONE.shiftLeft(t);
                        return this.bitwiseTo(r, e, r),
                        r
                    }
                    ,
                    F.prototype.addTo = function(t, e) {
                        for (var r = 0, n = 0, i = Math.min(t.t, this.t); r < i; )
                            n += this[r] + t[r],
                            e[r++] = n & this.DM,
                            n >>= this.DB;
                        if (t.t < this.t) {
                            for (n += t.s; r < this.t; )
                                n += this[r],
                                e[r++] = n & this.DM,
                                n >>= this.DB;
                            n += this.s
                        } else {
                            for (n += this.s; r < t.t; )
                                n += t[r],
                                e[r++] = n & this.DM,
                                n >>= this.DB;
                            n += t.s
                        }
                        e.s = n < 0 ? -1 : 0,
                        n > 0 ? e[r++] = n : n < -1 && (e[r++] = this.DV + n),
                        e.t = r,
                        e.clamp()
                    }
                    ,
                    F.prototype.dMultiply = function(t) {
                        this[this.t] = this.am(0, t - 1, this, 0, 0, this.t),
                        ++this.t,
                        this.clamp()
                    }
                    ,
                    F.prototype.dAddOffset = function(t, e) {
                        if (0 != t) {
                            for (; this.t <= e; )
                                this[this.t++] = 0;
                            for (this[e] += t; this[e] >= this.DV; )
                                this[e] -= this.DV,
                                ++e >= this.t && (this[this.t++] = 0),
                                ++this[e]
                        }
                    }
                    ,
                    F.prototype.multiplyLowerTo = function(t, e, r) {
                        var n, i = Math.min(this.t + t.t, e);
                        for (r.s = 0,
                        r.t = i; i > 0; )
                            r[--i] = 0;
                        for (n = r.t - this.t; i < n; ++i)
                            r[i + this.t] = this.am(0, t[i], r, i, 0, this.t);
                        for (n = Math.min(t.t, e); i < n; ++i)
                            this.am(0, t[i], r, i, 0, e - i);
                        r.clamp()
                    }
                    ,
                    F.prototype.multiplyUpperTo = function(t, e, r) {
                        --e;
                        var n = r.t = this.t + t.t - e;
                        for (r.s = 0; --n >= 0; )
                            r[n] = 0;
                        for (n = Math.max(e - this.t, 0); n < t.t; ++n)
                            r[this.t + n - e] = this.am(e - n, t[n], r, 0, 0, this.t + n - e);
                        r.clamp(),
                        r.drShiftTo(1, r)
                    }
                    ,
                    F.prototype.modInt = function(t) {
                        if (t <= 0)
                            return 0;
                        var e = this.DV % t
                          , r = this.s < 0 ? t - 1 : 0;
                        if (this.t > 0)
                            if (0 == e)
                                r = this[0] % t;
                            else
                                for (var n = this.t - 1; n >= 0; --n)
                                    r = (e * r + this[n]) % t;
                        return r
                    }
                    ,
                    F.prototype.millerRabin = function(t) {
                        var e = this.subtract(F.ONE)
                          , r = e.getLowestSetBit();
                        if (r <= 0)
                            return !1;
                        var n = e.shiftRight(r);
                        (t = t + 1 >> 1) > W.length && (t = W.length);
                        for (var i = E(), o = 0; o < t; ++o) {
                            i.fromInt(W[Math.floor(Math.random() * W.length)]);
                            var s = i.modPow(n, this);
                            if (0 != s.compareTo(F.ONE) && 0 != s.compareTo(e)) {
                                for (var a = 1; a++ < r && 0 != s.compareTo(e); )
                                    if (0 == (s = s.modPowInt(2, this)).compareTo(F.ONE))
                                        return !1;
                                if (0 != s.compareTo(e))
                                    return !1
                            }
                        }
                        return !0
                    }
                    ,
                    F.prototype.clone = function() {
                        var t = E();
                        return this.copyTo(t),
                        t
                    }
                    ,
                    F.prototype.intValue = function() {
                        if (this.s < 0) {
                            if (1 == this.t)
                                return this[0] - this.DV;
                            if (0 == this.t)
                                return -1
                        } else {
                            if (1 == this.t)
                                return this[0];
                            if (0 == this.t)
                                return 0
                        }
                        return (this[1] & (1 << 32 - this.DB) - 1) << this.DB | this[0]
                    }
                    ,
                    F.prototype.byteValue = function() {
                        return 0 == this.t ? this.s : this[0] << 24 >> 24
                    }
                    ,
                    F.prototype.shortValue = function() {
                        return 0 == this.t ? this.s : this[0] << 16 >> 16
                    }
                    ,
                    F.prototype.signum = function() {
                        return this.s < 0 ? -1 : this.t <= 0 || 1 == this.t && this[0] <= 0 ? 0 : 1
                    }
                    ,
                    F.prototype.toByteArray = function() {
                        var t = this.t
                          , e = new Array;
                        e[0] = this.s;
                        var r, n = this.DB - t * this.DB % 8, i = 0;
                        if (t-- > 0)
                            for (n < this.DB && (r = this[t] >> n) != (this.s & this.DM) >> n && (e[i++] = r | this.s << this.DB - n); t >= 0; )
                                n < 8 ? (r = (this[t] & (1 << n) - 1) << 8 - n,
                                r |= this[--t] >> (n += this.DB - 8)) : (r = this[t] >> (n -= 8) & 255,
                                n <= 0 && (n += this.DB,
                                --t)),
                                0 != (128 & r) && (r |= -256),
                                0 == i && (128 & this.s) != (128 & r) && ++i,
                                (i > 0 || r != this.s) && (e[i++] = r);
                        return e
                    }
                    ,
                    F.prototype.equals = function(t) {
                        return 0 == this.compareTo(t)
                    }
                    ,
                    F.prototype.min = function(t) {
                        return this.compareTo(t) < 0 ? this : t
                    }
                    ,
                    F.prototype.max = function(t) {
                        return this.compareTo(t) > 0 ? this : t
                    }
                    ,
                    F.prototype.and = function(t) {
                        var e = E();
                        return this.bitwiseTo(t, L, e),
                        e
                    }
                    ,
                    F.prototype.or = function(t) {
                        var e = E();
                        return this.bitwiseTo(t, N, e),
                        e
                    }
                    ,
                    F.prototype.xor = function(t) {
                        var e = E();
                        return this.bitwiseTo(t, U, e),
                        e
                    }
                    ,
                    F.prototype.andNot = function(t) {
                        var e = E();
                        return this.bitwiseTo(t, O, e),
                        e
                    }
                    ,
                    F.prototype.not = function() {
                        for (var t = E(), e = 0; e < this.t; ++e)
                            t[e] = this.DM & ~this[e];
                        return t.t = this.t,
                        t.s = ~this.s,
                        t
                    }
                    ,
                    F.prototype.shiftLeft = function(t) {
                        var e = E();
                        return t < 0 ? this.rShiftTo(-t, e) : this.lShiftTo(t, e),
                        e
                    }
                    ,
                    F.prototype.shiftRight = function(t) {
                        var e = E();
                        return t < 0 ? this.lShiftTo(-t, e) : this.rShiftTo(t, e),
                        e
                    }
                    ,
                    F.prototype.getLowestSetBit = function() {
                        for (var t = 0; t < this.t; ++t)
                            if (0 != this[t])
                                return t * this.DB + B(this[t]);
                        return this.s < 0 ? this.t * this.DB : -1
                    }
                    ,
                    F.prototype.bitCount = function() {
                        for (var t = 0, e = this.s & this.DM, r = 0; r < this.t; ++r)
                            t += M(this[r] ^ e);
                        return t
                    }
                    ,
                    F.prototype.testBit = function(t) {
                        var e = Math.floor(t / this.DB);
                        return e >= this.t ? 0 != this.s : 0 != (this[e] & 1 << t % this.DB)
                    }
                    ,
                    F.prototype.setBit = function(t) {
                        return this.changeBit(t, N)
                    }
                    ,
                    F.prototype.clearBit = function(t) {
                        return this.changeBit(t, O)
                    }
                    ,
                    F.prototype.flipBit = function(t) {
                        return this.changeBit(t, U)
                    }
                    ,
                    F.prototype.add = function(t) {
                        var e = E();
                        return this.addTo(t, e),
                        e
                    }
                    ,
                    F.prototype.subtract = function(t) {
                        var e = E();
                        return this.subTo(t, e),
                        e
                    }
                    ,
                    F.prototype.multiply = function(t) {
                        var e = E();
                        return this.multiplyTo(t, e),
                        e
                    }
                    ,
                    F.prototype.divide = function(t) {
                        var e = E();
                        return this.divRemTo(t, e, null),
                        e
                    }
                    ,
                    F.prototype.remainder = function(t) {
                        var e = E();
                        return this.divRemTo(t, null, e),
                        e
                    }
                    ,
                    F.prototype.divideAndRemainder = function(t) {
                        var e = E()
                          , r = E();
                        return this.divRemTo(t, e, r),
                        new Array(e,r)
                    }
                    ,
                    F.prototype.modPow = function(t, e) {
                        var r, n, i = t.bitLength(), o = T(1);
                        if (i <= 0)
                            return o;
                        r = i < 18 ? 1 : i < 48 ? 3 : i < 144 ? 4 : i < 768 ? 5 : 6,
                        n = i < 8 ? new I(e) : e.isEven() ? new K(e) : new D(e);
                        var s = new Array
                          , a = 3
                          , u = r - 1
                          , c = (1 << r) - 1;
                        if (s[1] = n.convert(this),
                        r > 1) {
                            var h = E();
                            for (n.sqrTo(s[1], h); a <= c; )
                                s[a] = E(),
                                n.mulTo(h, s[a - 2], s[a]),
                                a += 2
                        }
                        var l, f, g = t.t - 1, d = !0, p = E();
                        for (i = R(t[g]) - 1; g >= 0; ) {
                            for (i >= u ? l = t[g] >> i - u & c : (l = (t[g] & (1 << i + 1) - 1) << u - i,
                            g > 0 && (l |= t[g - 1] >> this.DB + i - u)),
                            a = r; 0 == (1 & l); )
                                l >>= 1,
                                --a;
                            if ((i -= a) < 0 && (i += this.DB,
                            --g),
                            d)
                                s[l].copyTo(o),
                                d = !1;
                            else {
                                for (; a > 1; )
                                    n.sqrTo(o, p),
                                    n.sqrTo(p, o),
                                    a -= 2;
                                a > 0 ? n.sqrTo(o, p) : (f = o,
                                o = p,
                                p = f),
                                n.mulTo(p, s[l], o)
                            }
                            for (; g >= 0 && 0 == (t[g] & 1 << i); )
                                n.sqrTo(o, p),
                                f = o,
                                o = p,
                                p = f,
                                --i < 0 && (i = this.DB - 1,
                                --g)
                        }
                        return n.revert(o)
                    }
                    ,
                    F.prototype.modInverse = function(t) {
                        var e = t.isEven();
                        if (this.isEven() && e || 0 == t.signum())
                            return F.ZERO;
                        for (var r = t.clone(), n = this.clone(), i = T(1), o = T(0), s = T(0), a = T(1); 0 != r.signum(); ) {
                            for (; r.isEven(); )
                                r.rShiftTo(1, r),
                                e ? (i.isEven() && o.isEven() || (i.addTo(this, i),
                                o.subTo(t, o)),
                                i.rShiftTo(1, i)) : o.isEven() || o.subTo(t, o),
                                o.rShiftTo(1, o);
                            for (; n.isEven(); )
                                n.rShiftTo(1, n),
                                e ? (s.isEven() && a.isEven() || (s.addTo(this, s),
                                a.subTo(t, a)),
                                s.rShiftTo(1, s)) : a.isEven() || a.subTo(t, a),
                                a.rShiftTo(1, a);
                            r.compareTo(n) >= 0 ? (r.subTo(n, r),
                            e && i.subTo(s, i),
                            o.subTo(a, o)) : (n.subTo(r, n),
                            e && s.subTo(i, s),
                            a.subTo(o, a))
                        }
                        return 0 != n.compareTo(F.ONE) ? F.ZERO : a.compareTo(t) >= 0 ? a.subtract(t) : a.signum() < 0 ? (a.addTo(t, a),
                        a.signum() < 0 ? a.add(t) : a) : a
                    }
                    ,
                    F.prototype.pow = function(t) {
                        return this.exp(t, new j)
                    }
                    ,
                    F.prototype.gcd = function(t) {
                        var e = this.s < 0 ? this.negate() : this.clone()
                          , r = t.s < 0 ? t.negate() : t.clone();
                        if (e.compareTo(r) < 0) {
                            var n = e;
                            e = r,
                            r = n
                        }
                        var i = e.getLowestSetBit()
                          , o = r.getLowestSetBit();
                        if (o < 0)
                            return e;
                        for (i < o && (o = i),
                        o > 0 && (e.rShiftTo(o, e),
                        r.rShiftTo(o, r)); e.signum() > 0; )
                            (i = e.getLowestSetBit()) > 0 && e.rShiftTo(i, e),
                            (i = r.getLowestSetBit()) > 0 && r.rShiftTo(i, r),
                            e.compareTo(r) >= 0 ? (e.subTo(r, e),
                            e.rShiftTo(1, e)) : (r.subTo(e, r),
                            r.rShiftTo(1, r));
                        return o > 0 && r.lShiftTo(o, r),
                        r
                    }
                    ,
                    F.prototype.isProbablePrime = function(t) {
                        var e, r = this.abs();
                        if (1 == r.t && r[0] <= W[W.length - 1]) {
                            for (e = 0; e < W.length; ++e)
                                if (r[0] == W[e])
                                    return !0;
                            return !1
                        }
                        if (r.isEven())
                            return !1;
                        for (e = 1; e < W.length; ) {
                            for (var n = W[e], i = e + 1; i < W.length && n < z; )
                                n *= W[i++];
                            for (n = r.modInt(n); e < i; )
                                if (n % W[e++] == 0)
                                    return !1
                        }
                        return r.millerRabin(t)
                    }
                    ,
                    F.prototype.square = function() {
                        var t = E();
                        return this.squareTo(t),
                        t
                    }
                    ,
                    Y.prototype.init = function(t) {
                        var e, r, n;
                        for (e = 0; e < 256; ++e)
                            this.S[e] = e;
                        for (r = 0,
                        e = 0; e < 256; ++e)
                            r = r + this.S[e] + t[e % t.length] & 255,
                            n = this.S[e],
                            this.S[e] = this.S[r],
                            this.S[r] = n;
                        this.i = 0,
                        this.j = 0
                    }
                    ,
                    Y.prototype.next = function() {
                        var t;
                        return this.i = this.i + 1 & 255,
                        this.j = this.j + this.S[this.i] & 255,
                        t = this.S[this.i],
                        this.S[this.i] = this.S[this.j],
                        this.S[this.j] = t,
                        this.S[t + this.S[this.i] & 255]
                    }
                    ,
                    null == q) {
                        var X;
                        if (q = new Array,
                        J = 0,
                        void 0 !== i && (void 0 !== i.crypto || void 0 !== i.msCrypto)) {
                            var $ = i.crypto || i.msCrypto;
                            if ($.getRandomValues) {
                                var Q = new Uint8Array(32);
                                for ($.getRandomValues(Q),
                                X = 0; X < 32; ++X)
                                    q[J++] = Q[X]
                            } else if ("Netscape" == n.appName && n.appVersion < "5") {
                                var Z = i.crypto.random(32);
                                for (X = 0; X < Z.length; ++X)
                                    q[J++] = 255 & Z.charCodeAt(X)
                            }
                        }
                        for (; J < 256; )
                            X = Math.floor(65536 * Math.random()),
                            q[J++] = X >>> 8,
                            q[J++] = 255 & X;
                        J = 0,
                        G()
                    }
                    function tt() {
                        if (null == V) {
                            for (G(),
                            (V = new Y).init(q),
                            J = 0; J < q.length; ++J)
                                q[J] = 0;
                            J = 0
                        }
                        return V.next()
                    }
                    function et() {}
                    function rt(t, e) {
                        return new F(t,e)
                    }
                    function nt(t, e, r) {
                        for (var n = "", i = 0; n.length < e; )
                            n += r(String.fromCharCode.apply(String, t.concat([(4278190080 & i) >> 24, (16711680 & i) >> 16, (65280 & i) >> 8, 255 & i]))),
                            i += 1;
                        return n
                    }
                    function it() {
                        this.n = null,
                        this.e = 0,
                        this.d = null,
                        this.p = null,
                        this.q = null,
                        this.dmp1 = null,
                        this.dmq1 = null,
                        this.coeff = null
                    }
                    function ot(t, e) {
                        this.x = e,
                        this.q = t
                    }
                    function st(t, e, r, n) {
                        this.curve = t,
                        this.x = e,
                        this.y = r,
                        this.z = null == n ? F.ONE : n,
                        this.zinv = null
                    }
                    function at(t, e, r) {
                        this.q = t,
                        this.a = this.fromBigInteger(e),
                        this.b = this.fromBigInteger(r),
                        this.infinity = new st(this,null,null)
                    }
                    et.prototype.nextBytes = function(t) {
                        var e;
                        for (e = 0; e < t.length; ++e)
                            t[e] = tt()
                    }
                    ,
                    it.prototype.doPublic = function(t) {
                        return t.modPowInt(this.e, this.n)
                    }
                    ,
                    it.prototype.setPublic = function(t, e) {
                        if (this.isPublic = !0,
                        this.isPrivate = !1,
                        "string" != typeof t)
                            this.n = t,
                            this.e = e;
                        else {
                            if (!(null != t && null != e && t.length > 0 && e.length > 0))
                                throw "Invalid RSA public key";
                            this.n = rt(t, 16),
                            this.e = parseInt(e, 16)
                        }
                    }
                    ,
                    it.prototype.encrypt = function(t) {
                        var e = function(t, e) {
                            if (e < t.length + 11)
                                throw "Message too long for RSA";
                            for (var r = new Array, n = t.length - 1; n >= 0 && e > 0; ) {
                                var i = t.charCodeAt(n--);
                                i < 128 ? r[--e] = i : i > 127 && i < 2048 ? (r[--e] = 63 & i | 128,
                                r[--e] = i >> 6 | 192) : (r[--e] = 63 & i | 128,
                                r[--e] = i >> 6 & 63 | 128,
                                r[--e] = i >> 12 | 224)
                            }
                            r[--e] = 0;
                            for (var o = new et, s = new Array; e > 2; ) {
                                for (s[0] = 0; 0 == s[0]; )
                                    o.nextBytes(s);
                                r[--e] = s[0]
                            }
                            return r[--e] = 2,
                            r[--e] = 0,
                            new F(r)
                        }(t, this.n.bitLength() + 7 >> 3);
                        if (null == e)
                            return null;
                        var r = this.doPublic(e);
                        if (null == r)
                            return null;
                        var n = r.toString(16);
                        return 0 == (1 & n.length) ? n : "0" + n
                    }
                    ,
                    it.prototype.encryptOAEP = function(t, e, r) {
                        var n = function(t, e, r, n) {
                            var i = ct.crypto.MessageDigest
                              , o = ct.crypto.Util
                              , s = null;
                            if (r || (r = "sha1"),
                            "string" == typeof r && (s = i.getCanonicalAlgName(r),
                            n = i.getHashLength(s),
                            r = function(t) {
                                return Ft(o.hashHex(Et(t), s))
                            }
                            ),
                            t.length + 2 * n + 2 > e)
                                throw "Message too long for RSA";
                            var a, u = "";
                            for (a = 0; a < e - t.length - 2 * n - 2; a += 1)
                                u += "\0";
                            var c = r("") + u + "" + t
                              , h = new Array(n);
                            (new et).nextBytes(h);
                            var l = nt(h, c.length, r)
                              , f = [];
                            for (a = 0; a < c.length; a += 1)
                                f[a] = c.charCodeAt(a) ^ l.charCodeAt(a);
                            var g = nt(f, h.length, r)
                              , d = [0];
                            for (a = 0; a < h.length; a += 1)
                                d[a + 1] = h[a] ^ g.charCodeAt(a);
                            return new F(d.concat(f))
                        }(t, this.n.bitLength() + 7 >> 3, e, r);
                        if (null == n)
                            return null;
                        var i = this.doPublic(n);
                        if (null == i)
                            return null;
                        var o = i.toString(16);
                        return 0 == (1 & o.length) ? o : "0" + o
                    }
                    ,
                    it.prototype.type = "RSA",
                    ot.prototype.equals = function(t) {
                        return t == this || this.q.equals(t.q) && this.x.equals(t.x)
                    }
                    ,
                    ot.prototype.toBigInteger = function() {
                        return this.x
                    }
                    ,
                    ot.prototype.negate = function() {
                        return new ot(this.q,this.x.negate().mod(this.q))
                    }
                    ,
                    ot.prototype.add = function(t) {
                        return new ot(this.q,this.x.add(t.toBigInteger()).mod(this.q))
                    }
                    ,
                    ot.prototype.subtract = function(t) {
                        return new ot(this.q,this.x.subtract(t.toBigInteger()).mod(this.q))
                    }
                    ,
                    ot.prototype.multiply = function(t) {
                        return new ot(this.q,this.x.multiply(t.toBigInteger()).mod(this.q))
                    }
                    ,
                    ot.prototype.square = function() {
                        return new ot(this.q,this.x.square().mod(this.q))
                    }
                    ,
                    ot.prototype.divide = function(t) {
                        return new ot(this.q,this.x.multiply(t.toBigInteger().modInverse(this.q)).mod(this.q))
                    }
                    ,
                    st.prototype.getX = function() {
                        return null == this.zinv && (this.zinv = this.z.modInverse(this.curve.q)),
                        this.curve.fromBigInteger(this.x.toBigInteger().multiply(this.zinv).mod(this.curve.q))
                    }
                    ,
                    st.prototype.getY = function() {
                        return null == this.zinv && (this.zinv = this.z.modInverse(this.curve.q)),
                        this.curve.fromBigInteger(this.y.toBigInteger().multiply(this.zinv).mod(this.curve.q))
                    }
                    ,
                    st.prototype.equals = function(t) {
                        return t == this || (this.isInfinity() ? t.isInfinity() : t.isInfinity() ? this.isInfinity() : !!t.y.toBigInteger().multiply(this.z).subtract(this.y.toBigInteger().multiply(t.z)).mod(this.curve.q).equals(F.ZERO) && t.x.toBigInteger().multiply(this.z).subtract(this.x.toBigInteger().multiply(t.z)).mod(this.curve.q).equals(F.ZERO))
                    }
                    ,
                    st.prototype.isInfinity = function() {
                        return null == this.x && null == this.y || this.z.equals(F.ZERO) && !this.y.toBigInteger().equals(F.ZERO)
                    }
                    ,
                    st.prototype.negate = function() {
                        return new st(this.curve,this.x,this.y.negate(),this.z)
                    }
                    ,
                    st.prototype.add = function(t) {
                        if (this.isInfinity())
                            return t;
                        if (t.isInfinity())
                            return this;
                        var e = t.y.toBigInteger().multiply(this.z).subtract(this.y.toBigInteger().multiply(t.z)).mod(this.curve.q)
                          , r = t.x.toBigInteger().multiply(this.z).subtract(this.x.toBigInteger().multiply(t.z)).mod(this.curve.q);
                        if (F.ZERO.equals(r))
                            return F.ZERO.equals(e) ? this.twice() : this.curve.getInfinity();
                        var n = new F("3")
                          , i = this.x.toBigInteger()
                          , o = this.y.toBigInteger()
                          , s = (t.x.toBigInteger(),
                        t.y.toBigInteger(),
                        r.square())
                          , a = s.multiply(r)
                          , u = i.multiply(s)
                          , c = e.square().multiply(this.z)
                          , h = c.subtract(u.shiftLeft(1)).multiply(t.z).subtract(a).multiply(r).mod(this.curve.q)
                          , l = u.multiply(n).multiply(e).subtract(o.multiply(a)).subtract(c.multiply(e)).multiply(t.z).add(e.multiply(a)).mod(this.curve.q)
                          , f = a.multiply(this.z).multiply(t.z).mod(this.curve.q);
                        return new st(this.curve,this.curve.fromBigInteger(h),this.curve.fromBigInteger(l),f)
                    }
                    ,
                    st.prototype.twice = function() {
                        if (this.isInfinity())
                            return this;
                        if (0 == this.y.toBigInteger().signum())
                            return this.curve.getInfinity();
                        var t = new F("3")
                          , e = this.x.toBigInteger()
                          , r = this.y.toBigInteger()
                          , n = r.multiply(this.z)
                          , i = n.multiply(r).mod(this.curve.q)
                          , o = this.curve.a.toBigInteger()
                          , s = e.square().multiply(t);
                        F.ZERO.equals(o) || (s = s.add(this.z.square().multiply(o)));
                        var a = (s = s.mod(this.curve.q)).square().subtract(e.shiftLeft(3).multiply(i)).shiftLeft(1).multiply(n).mod(this.curve.q)
                          , u = s.multiply(t).multiply(e).subtract(i.shiftLeft(1)).shiftLeft(2).multiply(i).subtract(s.square().multiply(s)).mod(this.curve.q)
                          , c = n.square().multiply(n).shiftLeft(3).mod(this.curve.q);
                        return new st(this.curve,this.curve.fromBigInteger(a),this.curve.fromBigInteger(u),c)
                    }
                    ,
                    st.prototype.multiply = function(t) {
                        if (this.isInfinity())
                            return this;
                        if (0 == t.signum())
                            return this.curve.getInfinity();
                        var e, r = t, n = r.multiply(new F("3")), i = this.negate(), o = this, s = this.curve.q.subtract(t), a = s.multiply(new F("3")), u = new st(this.curve,this.x,this.y), c = u.negate();
                        for (e = n.bitLength() - 2; e > 0; --e) {
                            o = o.twice();
                            var h = n.testBit(e);
                            h != r.testBit(e) && (o = o.add(h ? this : i))
                        }
                        for (e = a.bitLength() - 2; e > 0; --e) {
                            u = u.twice();
                            var l = a.testBit(e);
                            l != s.testBit(e) && (u = u.add(l ? u : c))
                        }
                        return o
                    }
                    ,
                    st.prototype.multiplyTwo = function(t, e, r) {
                        var n;
                        n = t.bitLength() > r.bitLength() ? t.bitLength() - 1 : r.bitLength() - 1;
                        for (var i = this.curve.getInfinity(), o = this.add(e); n >= 0; )
                            i = i.twice(),
                            t.testBit(n) ? i = r.testBit(n) ? i.add(o) : i.add(this) : r.testBit(n) && (i = i.add(e)),
                            --n;
                        return i
                    }
                    ,
                    at.prototype.getQ = function() {
                        return this.q
                    }
                    ,
                    at.prototype.getA = function() {
                        return this.a
                    }
                    ,
                    at.prototype.getB = function() {
                        return this.b
                    }
                    ,
                    at.prototype.equals = function(t) {
                        return t == this || this.q.equals(t.q) && this.a.equals(t.a) && this.b.equals(t.b)
                    }
                    ,
                    at.prototype.getInfinity = function() {
                        return this.infinity
                    }
                    ,
                    at.prototype.fromBigInteger = function(t) {
                        return new ot(this.q,t)
                    }
                    ,
                    at.prototype.decodePointHex = function(t) {
                        switch (parseInt(t.substr(0, 2), 16)) {
                        case 0:
                            return this.infinity;
                        case 2:
                        case 3:
                            return null;
                        case 4:
                        case 6:
                        case 7:
                            var e = (t.length - 2) / 2
                              , r = t.substr(2, e)
                              , n = t.substr(e + 2, e);
                            return new st(this,this.fromBigInteger(new F(r,16)),this.fromBigInteger(new F(n,16)));
                        default:
                            return null
                        }
                    }
                    ,
                    ot.prototype.getByteLength = function() {
                        return Math.floor((this.toBigInteger().bitLength() + 7) / 8)
                    }
                    ,
                    st.prototype.getEncoded = function(t) {
                        var e = function(t, e) {
                            var r = t.toByteArrayUnsigned();
                            if (e < r.length)
                                r = r.slice(r.length - e);
                            else
                                for (; e > r.length; )
                                    r.unshift(0);
                            return r
                        }
                          , r = this.getX().toBigInteger()
                          , n = this.getY().toBigInteger()
                          , i = e(r, 32);
                        return t ? n.isEven() ? i.unshift(2) : i.unshift(3) : (i.unshift(4),
                        i = i.concat(e(n, 32))),
                        i
                    }
                    ,
                    st.decodeFrom = function(t, e) {
                        e[0];
                        var r = e.length - 1
                          , n = e.slice(1, 1 + r / 2)
                          , i = e.slice(1 + r / 2, 1 + r);
                        n.unshift(0),
                        i.unshift(0);
                        var o = new F(n)
                          , s = new F(i);
                        return new st(t,t.fromBigInteger(o),t.fromBigInteger(s))
                    }
                    ,
                    st.decodeFromHex = function(t, e) {
                        e.substr(0, 2);
                        var r = e.length - 2
                          , n = e.substr(2, r / 2)
                          , i = e.substr(2 + r / 2, r / 2)
                          , o = new F(n,16)
                          , s = new F(i,16);
                        return new st(t,t.fromBigInteger(o),t.fromBigInteger(s))
                    }
                    ,
                    st.prototype.add2D = function(t) {
                        if (this.isInfinity())
                            return t;
                        if (t.isInfinity())
                            return this;
                        if (this.x.equals(t.x))
                            return this.y.equals(t.y) ? this.twice() : this.curve.getInfinity();
                        var e = t.x.subtract(this.x)
                          , r = t.y.subtract(this.y).divide(e)
                          , n = r.square().subtract(this.x).subtract(t.x)
                          , i = r.multiply(this.x.subtract(n)).subtract(this.y);
                        return new st(this.curve,n,i)
                    }
                    ,
                    st.prototype.twice2D = function() {
                        if (this.isInfinity())
                            return this;
                        if (0 == this.y.toBigInteger().signum())
                            return this.curve.getInfinity();
                        var t = this.curve.fromBigInteger(F.valueOf(2))
                          , e = this.curve.fromBigInteger(F.valueOf(3))
                          , r = this.x.square().multiply(e).add(this.curve.a).divide(this.y.multiply(t))
                          , n = r.square().subtract(this.x.multiply(t))
                          , i = r.multiply(this.x.subtract(n)).subtract(this.y);
                        return new st(this.curve,n,i)
                    }
                    ,
                    st.prototype.multiply2D = function(t) {
                        if (this.isInfinity())
                            return this;
                        if (0 == t.signum())
                            return this.curve.getInfinity();
                        var e, r = t, n = r.multiply(new F("3")), i = this.negate(), o = this;
                        for (e = n.bitLength() - 2; e > 0; --e) {
                            o = o.twice();
                            var s = n.testBit(e);
                            s != r.testBit(e) && (o = o.add2D(s ? this : i))
                        }
                        return o
                    }
                    ,
                    st.prototype.isOnCurve = function() {
                        var t = this.getX().toBigInteger()
                          , e = this.getY().toBigInteger()
                          , r = this.curve.getA().toBigInteger()
                          , n = this.curve.getB().toBigInteger()
                          , i = this.curve.getQ()
                          , o = e.multiply(e).mod(i)
                          , s = t.multiply(t).multiply(t).add(r.multiply(t)).add(n).mod(i);
                        return o.equals(s)
                    }
                    ,
                    st.prototype.toString = function() {
                        return "(" + this.getX().toBigInteger().toString() + "," + this.getY().toBigInteger().toString() + ")"
                    }
                    ,
                    st.prototype.validate = function() {
                        var t = this.curve.getQ();
                        if (this.isInfinity())
                            throw new Error("Point is at infinity.");
                        var e = this.getX().toBigInteger()
                          , r = this.getY().toBigInteger();
                        if (e.compareTo(F.ONE) < 0 || e.compareTo(t.subtract(F.ONE)) > 0)
                            throw new Error("x coordinate out of bounds");
                        if (r.compareTo(F.ONE) < 0 || r.compareTo(t.subtract(F.ONE)) > 0)
                            throw new Error("y coordinate out of bounds");
                        if (!this.isOnCurve())
                            throw new Error("Point is not on the curve.");
                        if (this.multiply(t).isInfinity())
                            throw new Error("Point is not a scalar multiple of G.");
                        return !0
                    }
                    ;
                    var ut = function() {
                        var t = new RegExp('(?:false|true|null|[\\{\\}\\[\\]]|(?:-?\\b(?:0|[1-9][0-9]*)(?:\\.[0-9]+)?(?:[eE][+-]?[0-9]+)?\\b)|(?:"(?:[^\\0-\\x08\\x0a-\\x1f"\\\\]|\\\\(?:["/\\\\bfnrt]|u[0-9A-Fa-f]{4}))*"))',"g")
                          , e = new RegExp("\\\\(?:([^u])|u(.{4}))","g")
                          , n = {
                            '"': '"',
                            "/": "/",
                            "\\": "\\",
                            b: "\b",
                            f: "\f",
                            n: "\n",
                            r: "\r",
                            t: "\t"
                        };
                        function i(t, e, r) {
                            return e ? n[e] : String.fromCharCode(parseInt(r, 16))
                        }
                        var o = new String("")
                          , s = Object.hasOwnProperty;
                        return function(n, a) {
                            var u, c, h = n.match(t), l = h[0], f = !1;
                            "{" === l ? u = {} : "[" === l ? u = [] : (u = [],
                            f = !0);
                            for (var g = [u], d = 1 - f, p = h.length; d < p; ++d) {
                                var v;
                                switch ((l = h[d]).charCodeAt(0)) {
                                default:
                                    (v = g[0])[c || v.length] = +l,
                                    c = void 0;
                                    break;
                                case 34:
                                    if (-1 !== (l = l.substring(1, l.length - 1)).indexOf("\\") && (l = l.replace(e, i)),
                                    v = g[0],
                                    !c) {
                                        if (!(v instanceof Array)) {
                                            c = l || o;
                                            break
                                        }
                                        c = v.length
                                    }
                                    v[c] = l,
                                    c = void 0;
                                    break;
                                case 91:
                                    v = g[0],
                                    g.unshift(v[c || v.length] = []),
                                    c = void 0;
                                    break;
                                case 93:
                                    g.shift();
                                    break;
                                case 102:
                                    (v = g[0])[c || v.length] = !1,
                                    c = void 0;
                                    break;
                                case 110:
                                    (v = g[0])[c || v.length] = null,
                                    c = void 0;
                                    break;
                                case 116:
                                    (v = g[0])[c || v.length] = !0,
                                    c = void 0;
                                    break;
                                case 123:
                                    v = g[0],
                                    g.unshift(v[c || v.length] = {}),
                                    c = void 0;
                                    break;
                                case 125:
                                    g.shift()
                                }
                            }
                            if (f) {
                                if (1 !== g.length)
                                    throw new Error;
                                u = u[0]
                            } else if (g.length)
                                throw new Error;
                            return a && (u = function t(e, n) {
                                var i = e[n];
                                if (i && "object" === (void 0 === i ? "undefined" : r(i))) {
                                    var o = null;
                                    for (var u in i)
                                        if (s.call(i, u) && i !== e) {
                                            var c = t(i, u);
                                            void 0 !== c ? i[u] = c : (o || (o = []),
                                            o.push(u))
                                        }
                                    if (o)
                                        for (var h = o.length; --h >= 0; )
                                            delete i[o[h]]
                                }
                                return a.call(e, n, i)
                            }({
                                "": u
                            }, "")),
                            u
                        }
                    }();
                    void 0 !== ct && ct || (e.KJUR = ct = {}),
                    void 0 !== ct.asn1 && ct.asn1 || (ct.asn1 = {}),
                    ct.asn1.ASN1Util = new function() {
                        this.integerToByteHex = function(t) {
                            var e = t.toString(16);
                            return e.length % 2 == 1 && (e = "0" + e),
                            e
                        }
                        ,
                        this.bigIntToMinTwosComplementsHex = function(t) {
                            var e = t.toString(16);
                            if ("-" != e.substr(0, 1))
                                e.length % 2 == 1 ? e = "0" + e : e.match(/^[0-7]/) || (e = "00" + e);
                            else {
                                var r = e.substr(1).length;
                                r % 2 == 1 ? r += 1 : e.match(/^[0-7]/) || (r += 2);
                                for (var n = "", i = 0; i < r; i++)
                                    n += "f";
                                e = new F(n,16).xor(t).add(F.ONE).toString(16).replace(/^-/, "")
                            }
                            return e
                        }
                        ,
                        this.getPEMStringFromHex = function(t, e) {
                            return Pt(t, e)
                        }
                        ,
                        this.newObject = function(t) {
                            var e = ct.asn1
                              , r = e.ASN1Object
                              , n = e.DERBoolean
                              , i = e.DERInteger
                              , o = e.DERBitString
                              , s = e.DEROctetString
                              , a = e.DERNull
                              , u = e.DERObjectIdentifier
                              , c = e.DEREnumerated
                              , h = e.DERUTF8String
                              , l = e.DERNumericString
                              , f = e.DERPrintableString
                              , g = e.DERTeletexString
                              , d = e.DERIA5String
                              , p = e.DERUTCTime
                              , v = e.DERGeneralizedTime
                              , y = e.DERVisibleString
                              , m = e.DERBMPString
                              , _ = e.DERSequence
                              , S = e.DERSet
                              , w = e.DERTaggedObject
                              , b = e.ASN1Util.newObject;
                            if (t instanceof e.ASN1Object)
                                return t;
                            var F = Object.keys(t);
                            if (1 != F.length)
                                throw new Error("key of param shall be only one.");
                            var E = F[0];
                            if (-1 == ":asn1:bool:int:bitstr:octstr:null:oid:enum:utf8str:numstr:prnstr:telstr:ia5str:utctime:gentime:visstr:bmpstr:seq:set:tag:".indexOf(":" + E + ":"))
                                throw new Error("undefined key: " + E);
                            if ("bool" == E)
                                return new n(t[E]);
                            if ("int" == E)
                                return new i(t[E]);
                            if ("bitstr" == E)
                                return new o(t[E]);
                            if ("octstr" == E)
                                return new s(t[E]);
                            if ("null" == E)
                                return new a(t[E]);
                            if ("oid" == E)
                                return new u(t[E]);
                            if ("enum" == E)
                                return new c(t[E]);
                            if ("utf8str" == E)
                                return new h(t[E]);
                            if ("numstr" == E)
                                return new l(t[E]);
                            if ("prnstr" == E)
                                return new f(t[E]);
                            if ("telstr" == E)
                                return new g(t[E]);
                            if ("ia5str" == E)
                                return new d(t[E]);
                            if ("utctime" == E)
                                return new p(t[E]);
                            if ("gentime" == E)
                                return new v(t[E]);
                            if ("visstr" == E)
                                return new y(t[E]);
                            if ("bmpstr" == E)
                                return new m(t[E]);
                            if ("asn1" == E)
                                return new r(t[E]);
                            if ("seq" == E) {
                                for (var x = t[E], A = [], k = 0; k < x.length; k++) {
                                    var P = b(x[k]);
                                    A.push(P)
                                }
                                return new _({
                                    array: A
                                })
                            }
                            if ("set" == E) {
                                for (x = t[E],
                                A = [],
                                k = 0; k < x.length; k++)
                                    P = b(x[k]),
                                    A.push(P);
                                return new S({
                                    array: A
                                })
                            }
                            if ("tag" == E) {
                                var C = t[E];
                                if ("[object Array]" === Object.prototype.toString.call(C) && 3 == C.length) {
                                    var T = b(C[2]);
                                    return new w({
                                        tag: C[0],
                                        explicit: C[1],
                                        obj: T
                                    })
                                }
                                return new w(C)
                            }
                        }
                        ,
                        this.jsonToASN1HEX = function(t) {
                            return this.newObject(t).getEncodedHex()
                        }
                    }
                    ,
                    ct.asn1.ASN1Util.oidHexToInt = function(t) {
                        for (var e = "", r = parseInt(t.substr(0, 2), 16), n = (e = Math.floor(r / 40) + "." + r % 40,
                        ""), i = 2; i < t.length; i += 2) {
                            var o = ("00000000" + parseInt(t.substr(i, 2), 16).toString(2)).slice(-8);
                            n += o.substr(1, 7),
                            "0" == o.substr(0, 1) && (e = e + "." + new F(n,2).toString(10),
                            n = "")
                        }
                        return e
                    }
                    ,
                    ct.asn1.ASN1Util.oidIntToHex = function(t) {
                        var e = function(t) {
                            var e = t.toString(16);
                            return 1 == e.length && (e = "0" + e),
                            e
                        }
                          , r = function(t) {
                            var r = ""
                              , n = new F(t,10).toString(2)
                              , i = 7 - n.length % 7;
                            7 == i && (i = 0);
                            for (var o = "", s = 0; s < i; s++)
                                o += "0";
                            for (n = o + n,
                            s = 0; s < n.length - 1; s += 7) {
                                var a = n.substr(s, 7);
                                s != n.length - 7 && (a = "1" + a),
                                r += e(parseInt(a, 2))
                            }
                            return r
                        };
                        if (!t.match(/^[0-9.]+$/))
                            throw "malformed oid string: " + t;
                        var n = ""
                          , i = t.split(".")
                          , o = 40 * parseInt(i[0]) + parseInt(i[1]);
                        n += e(o),
                        i.splice(0, 2);
                        for (var s = 0; s < i.length; s++)
                            n += r(i[s]);
                        return n
                    }
                    ,
                    ct.asn1.ASN1Object = function(t) {
                        this.params = null,
                        this.getLengthHexFromValue = function() {
                            if (void 0 === this.hV || null == this.hV)
                                throw new Error("this.hV is null or undefined");
                            if (this.hV.length % 2 == 1)
                                throw new Error("value hex must be even length: n=" + "".length + ",v=" + this.hV);
                            var t = this.hV.length / 2
                              , e = t.toString(16);
                            if (e.length % 2 == 1 && (e = "0" + e),
                            t < 128)
                                return e;
                            var r = e.length / 2;
                            if (r > 15)
                                throw "ASN.1 length too long to represent by 8x: n = " + t.toString(16);
                            return (128 + r).toString(16) + e
                        }
                        ,
                        this.getEncodedHex = function() {
                            return (null == this.hTLV || this.isModified) && (this.hV = this.getFreshValueHex(),
                            this.hL = this.getLengthHexFromValue(),
                            this.hTLV = this.hT + this.hL + this.hV,
                            this.isModified = !1),
                            this.hTLV
                        }
                        ,
                        this.getValueHex = function() {
                            return this.getEncodedHex(),
                            this.hV
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return ""
                        }
                        ,
                        this.setByParam = function(t) {
                            this.params = t
                        }
                        ,
                        null != t && null != t.tlv && (this.hTLV = t.tlv,
                        this.isModified = !1)
                    }
                    ,
                    ct.asn1.DERAbstractString = function(t) {
                        ct.asn1.DERAbstractString.superclass.constructor.call(this),
                        this.getString = function() {
                            return this.s
                        }
                        ,
                        this.setString = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.s = t,
                            this.hV = wt(this.s).toLowerCase()
                        }
                        ,
                        this.setStringHex = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.s = null,
                            this.hV = t
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return this.hV
                        }
                        ,
                        void 0 !== t && ("string" == typeof t ? this.setString(t) : void 0 !== t.str ? this.setString(t.str) : void 0 !== t.hex && this.setStringHex(t.hex))
                    }
                    ,
                    o.lang.extend(ct.asn1.DERAbstractString, ct.asn1.ASN1Object),
                    ct.asn1.DERAbstractTime = function(t) {
                        ct.asn1.DERAbstractTime.superclass.constructor.call(this),
                        this.localDateToUTC = function(t) {
                            var e = t.getTime() + 6e4 * t.getTimezoneOffset();
                            return new Date(e)
                        }
                        ,
                        this.formatDate = function(t, e, r) {
                            var n = this.zeroPadding
                              , i = this.localDateToUTC(t)
                              , o = String(i.getFullYear());
                            "utc" == e && (o = o.substr(2, 2));
                            var s = o + n(String(i.getMonth() + 1), 2) + n(String(i.getDate()), 2) + n(String(i.getHours()), 2) + n(String(i.getMinutes()), 2) + n(String(i.getSeconds()), 2);
                            if (!0 === r) {
                                var a = i.getMilliseconds();
                                if (0 != a) {
                                    var u = n(String(a), 3);
                                    s = s + "." + (u = u.replace(/[0]+$/, ""))
                                }
                            }
                            return s + "Z"
                        }
                        ,
                        this.zeroPadding = function(t, e) {
                            return t.length >= e ? t : new Array(e - t.length + 1).join("0") + t
                        }
                        ,
                        this.getString = function() {
                            return this.s
                        }
                        ,
                        this.setString = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.s = t,
                            this.hV = vt(t)
                        }
                        ,
                        this.setByDateValue = function(t, e, r, n, i, o) {
                            var s = new Date(Date.UTC(t, e - 1, r, n, i, o, 0));
                            this.setByDate(s)
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return this.hV
                        }
                    }
                    ,
                    o.lang.extend(ct.asn1.DERAbstractTime, ct.asn1.ASN1Object),
                    ct.asn1.DERAbstractStructured = function(t) {
                        ct.asn1.DERAbstractString.superclass.constructor.call(this),
                        this.setByASN1ObjectArray = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.asn1Array = t
                        }
                        ,
                        this.appendASN1Object = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.asn1Array.push(t)
                        }
                        ,
                        this.asn1Array = new Array,
                        void 0 !== t && void 0 !== t.array && (this.asn1Array = t.array)
                    }
                    ,
                    o.lang.extend(ct.asn1.DERAbstractStructured, ct.asn1.ASN1Object),
                    ct.asn1.DERBoolean = function(t) {
                        ct.asn1.DERBoolean.superclass.constructor.call(this),
                        this.hT = "01",
                        this.hTLV = 0 == t ? "010100" : "0101ff"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERBoolean, ct.asn1.ASN1Object),
                    ct.asn1.DERInteger = function(t) {
                        ct.asn1.DERInteger.superclass.constructor.call(this),
                        this.hT = "02",
                        this.setByBigInteger = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.hV = ct.asn1.ASN1Util.bigIntToMinTwosComplementsHex(t)
                        }
                        ,
                        this.setByInteger = function(t) {
                            var e = new F(String(t),10);
                            this.setByBigInteger(e)
                        }
                        ,
                        this.setValueHex = function(t) {
                            this.hV = t
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return this.hV
                        }
                        ,
                        void 0 !== t && (void 0 !== t.bigint ? this.setByBigInteger(t.bigint) : void 0 !== t.int ? this.setByInteger(t.int) : "number" == typeof t ? this.setByInteger(t) : void 0 !== t.hex && this.setValueHex(t.hex))
                    }
                    ,
                    o.lang.extend(ct.asn1.DERInteger, ct.asn1.ASN1Object),
                    ct.asn1.DERBitString = function(t) {
                        if (void 0 !== t && void 0 !== t.obj) {
                            var e = ct.asn1.ASN1Util.newObject(t.obj);
                            t.hex = "00" + e.getEncodedHex()
                        }
                        ct.asn1.DERBitString.superclass.constructor.call(this),
                        this.hT = "03",
                        this.setHexValueIncludingUnusedBits = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.hV = t
                        }
                        ,
                        this.setUnusedBitsAndHexValue = function(t, e) {
                            if (t < 0 || 7 < t)
                                throw "unused bits shall be from 0 to 7: u = " + t;
                            var r = "0" + t;
                            this.hTLV = null,
                            this.isModified = !0,
                            this.hV = r + e
                        }
                        ,
                        this.setByBinaryString = function(t) {
                            var e = 8 - (t = t.replace(/0+$/, "")).length % 8;
                            8 == e && (e = 0);
                            for (var r = 0; r <= e; r++)
                                t += "0";
                            var n = "";
                            for (r = 0; r < t.length - 1; r += 8) {
                                var i = t.substr(r, 8)
                                  , o = parseInt(i, 2).toString(16);
                                1 == o.length && (o = "0" + o),
                                n += o
                            }
                            this.hTLV = null,
                            this.isModified = !0,
                            this.hV = "0" + e + n
                        }
                        ,
                        this.setByBooleanArray = function(t) {
                            for (var e = "", r = 0; r < t.length; r++)
                                1 == t[r] ? e += "1" : e += "0";
                            this.setByBinaryString(e)
                        }
                        ,
                        this.newFalseArray = function(t) {
                            for (var e = new Array(t), r = 0; r < t; r++)
                                e[r] = !1;
                            return e
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return this.hV
                        }
                        ,
                        void 0 !== t && ("string" == typeof t && t.toLowerCase().match(/^[0-9a-f]+$/) ? this.setHexValueIncludingUnusedBits(t) : void 0 !== t.hex ? this.setHexValueIncludingUnusedBits(t.hex) : void 0 !== t.bin ? this.setByBinaryString(t.bin) : void 0 !== t.array && this.setByBooleanArray(t.array))
                    }
                    ,
                    o.lang.extend(ct.asn1.DERBitString, ct.asn1.ASN1Object),
                    ct.asn1.DEROctetString = function(t) {
                        if (void 0 !== t && void 0 !== t.obj) {
                            var e = ct.asn1.ASN1Util.newObject(t.obj);
                            t.hex = e.getEncodedHex()
                        }
                        ct.asn1.DEROctetString.superclass.constructor.call(this, t),
                        this.hT = "04"
                    }
                    ,
                    o.lang.extend(ct.asn1.DEROctetString, ct.asn1.DERAbstractString),
                    ct.asn1.DERNull = function() {
                        ct.asn1.DERNull.superclass.constructor.call(this),
                        this.hT = "05",
                        this.hTLV = "0500"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERNull, ct.asn1.ASN1Object),
                    ct.asn1.DERObjectIdentifier = function(t) {
                        ct.asn1.DERObjectIdentifier.superclass.constructor.call(this),
                        this.hT = "06",
                        this.setValueHex = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.s = null,
                            this.hV = t
                        }
                        ,
                        this.setValueOidString = function(t) {
                            var e = function(t) {
                                var e = function(t) {
                                    var e = t.toString(16);
                                    return 1 == e.length && (e = "0" + e),
                                    e
                                }
                                  , r = function(t) {
                                    var r = ""
                                      , n = parseInt(t, 10).toString(2)
                                      , i = 7 - n.length % 7;
                                    7 == i && (i = 0);
                                    for (var o = "", s = 0; s < i; s++)
                                        o += "0";
                                    for (n = o + n,
                                    s = 0; s < n.length - 1; s += 7) {
                                        var a = n.substr(s, 7);
                                        s != n.length - 7 && (a = "1" + a),
                                        r += e(parseInt(a, 2))
                                    }
                                    return r
                                };
                                try {
                                    if (!t.match(/^[0-9.]+$/))
                                        return null;
                                    var n = ""
                                      , i = t.split(".")
                                      , o = 40 * parseInt(i[0], 10) + parseInt(i[1], 10);
                                    n += e(o),
                                    i.splice(0, 2);
                                    for (var s = 0; s < i.length; s++)
                                        n += r(i[s]);
                                    return n
                                } catch (t) {
                                    return null
                                }
                            }(t);
                            if (null == e)
                                throw new Error("malformed oid string: " + t);
                            this.hTLV = null,
                            this.isModified = !0,
                            this.s = null,
                            this.hV = e
                        }
                        ,
                        this.setValueName = function(t) {
                            var e = ct.asn1.x509.OID.name2oid(t);
                            if ("" === e)
                                throw new Error("DERObjectIdentifier oidName undefined: " + t);
                            this.setValueOidString(e)
                        }
                        ,
                        this.setValueNameOrOid = function(t) {
                            t.match(/^[0-2].[0-9.]+$/) ? this.setValueOidString(t) : this.setValueName(t)
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return this.hV
                        }
                        ,
                        this.setByParam = function(t) {
                            "string" == typeof t ? this.setValueNameOrOid(t) : void 0 !== t.oid ? this.setValueNameOrOid(t.oid) : void 0 !== t.name ? this.setValueNameOrOid(t.name) : void 0 !== t.hex && this.setValueHex(t.hex)
                        }
                        ,
                        void 0 !== t && this.setByParam(t)
                    }
                    ,
                    o.lang.extend(ct.asn1.DERObjectIdentifier, ct.asn1.ASN1Object),
                    ct.asn1.DEREnumerated = function(t) {
                        ct.asn1.DEREnumerated.superclass.constructor.call(this),
                        this.hT = "0a",
                        this.setByBigInteger = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.hV = ct.asn1.ASN1Util.bigIntToMinTwosComplementsHex(t)
                        }
                        ,
                        this.setByInteger = function(t) {
                            var e = new F(String(t),10);
                            this.setByBigInteger(e)
                        }
                        ,
                        this.setValueHex = function(t) {
                            this.hV = t
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return this.hV
                        }
                        ,
                        void 0 !== t && (void 0 !== t.int ? this.setByInteger(t.int) : "number" == typeof t ? this.setByInteger(t) : void 0 !== t.hex && this.setValueHex(t.hex))
                    }
                    ,
                    o.lang.extend(ct.asn1.DEREnumerated, ct.asn1.ASN1Object),
                    ct.asn1.DERUTF8String = function(t) {
                        ct.asn1.DERUTF8String.superclass.constructor.call(this, t),
                        this.hT = "0c"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERUTF8String, ct.asn1.DERAbstractString),
                    ct.asn1.DERNumericString = function(t) {
                        ct.asn1.DERNumericString.superclass.constructor.call(this, t),
                        this.hT = "12"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERNumericString, ct.asn1.DERAbstractString),
                    ct.asn1.DERPrintableString = function(t) {
                        ct.asn1.DERPrintableString.superclass.constructor.call(this, t),
                        this.hT = "13"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERPrintableString, ct.asn1.DERAbstractString),
                    ct.asn1.DERTeletexString = function(t) {
                        ct.asn1.DERTeletexString.superclass.constructor.call(this, t),
                        this.hT = "14"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERTeletexString, ct.asn1.DERAbstractString),
                    ct.asn1.DERIA5String = function(t) {
                        ct.asn1.DERIA5String.superclass.constructor.call(this, t),
                        this.hT = "16"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERIA5String, ct.asn1.DERAbstractString),
                    ct.asn1.DERVisibleString = function(t) {
                        ct.asn1.DERIA5String.superclass.constructor.call(this, t),
                        this.hT = "1a"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERVisibleString, ct.asn1.DERAbstractString),
                    ct.asn1.DERBMPString = function(t) {
                        ct.asn1.DERBMPString.superclass.constructor.call(this, t),
                        this.hT = "1e"
                    }
                    ,
                    o.lang.extend(ct.asn1.DERBMPString, ct.asn1.DERAbstractString),
                    ct.asn1.DERUTCTime = function(t) {
                        ct.asn1.DERUTCTime.superclass.constructor.call(this, t),
                        this.hT = "17",
                        this.setByDate = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.date = t,
                            this.s = this.formatDate(this.date, "utc"),
                            this.hV = vt(this.s)
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return void 0 === this.date && void 0 === this.s && (this.date = new Date,
                            this.s = this.formatDate(this.date, "utc"),
                            this.hV = vt(this.s)),
                            this.hV
                        }
                        ,
                        void 0 !== t && (void 0 !== t.str ? this.setString(t.str) : "string" == typeof t && t.match(/^[0-9]{12}Z$/) ? this.setString(t) : void 0 !== t.hex ? this.setStringHex(t.hex) : void 0 !== t.date && this.setByDate(t.date))
                    }
                    ,
                    o.lang.extend(ct.asn1.DERUTCTime, ct.asn1.DERAbstractTime),
                    ct.asn1.DERGeneralizedTime = function(t) {
                        ct.asn1.DERGeneralizedTime.superclass.constructor.call(this, t),
                        this.hT = "18",
                        this.withMillis = !1,
                        this.setByDate = function(t) {
                            this.hTLV = null,
                            this.isModified = !0,
                            this.date = t,
                            this.s = this.formatDate(this.date, "gen", this.withMillis),
                            this.hV = vt(this.s)
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return void 0 === this.date && void 0 === this.s && (this.date = new Date,
                            this.s = this.formatDate(this.date, "gen", this.withMillis),
                            this.hV = vt(this.s)),
                            this.hV
                        }
                        ,
                        void 0 !== t && (void 0 !== t.str ? this.setString(t.str) : "string" == typeof t && t.match(/^[0-9]{14}Z$/) ? this.setString(t) : void 0 !== t.hex ? this.setStringHex(t.hex) : void 0 !== t.date && this.setByDate(t.date),
                        !0 === t.millis && (this.withMillis = !0))
                    }
                    ,
                    o.lang.extend(ct.asn1.DERGeneralizedTime, ct.asn1.DERAbstractTime),
                    ct.asn1.DERSequence = function(t) {
                        ct.asn1.DERSequence.superclass.constructor.call(this, t),
                        this.hT = "30",
                        this.getFreshValueHex = function() {
                            for (var t = "", e = 0; e < this.asn1Array.length; e++)
                                t += this.asn1Array[e].getEncodedHex();
                            return this.hV = t,
                            this.hV
                        }
                    }
                    ,
                    o.lang.extend(ct.asn1.DERSequence, ct.asn1.DERAbstractStructured),
                    ct.asn1.DERSet = function(t) {
                        ct.asn1.DERSet.superclass.constructor.call(this, t),
                        this.hT = "31",
                        this.sortFlag = !0,
                        this.getFreshValueHex = function() {
                            for (var t = new Array, e = 0; e < this.asn1Array.length; e++) {
                                var r = this.asn1Array[e];
                                t.push(r.getEncodedHex())
                            }
                            return 1 == this.sortFlag && t.sort(),
                            this.hV = t.join(""),
                            this.hV
                        }
                        ,
                        void 0 !== t && void 0 !== t.sortflag && 0 == t.sortflag && (this.sortFlag = !1)
                    }
                    ,
                    o.lang.extend(ct.asn1.DERSet, ct.asn1.DERAbstractStructured),
                    ct.asn1.DERTaggedObject = function(t) {
                        ct.asn1.DERTaggedObject.superclass.constructor.call(this);
                        var e = ct.asn1;
                        this.hT = "a0",
                        this.hV = "",
                        this.isExplicit = !0,
                        this.asn1Object = null,
                        this.setASN1Object = function(t, e, r) {
                            this.hT = e,
                            this.isExplicit = t,
                            this.asn1Object = r,
                            this.isExplicit ? (this.hV = this.asn1Object.getEncodedHex(),
                            this.hTLV = null,
                            this.isModified = !0) : (this.hV = null,
                            this.hTLV = r.getEncodedHex(),
                            this.hTLV = this.hTLV.replace(/^../, e),
                            this.isModified = !1)
                        }
                        ,
                        this.getFreshValueHex = function() {
                            return this.hV
                        }
                        ,
                        this.setByParam = function(t) {
                            null != t.tag && (this.hT = t.tag),
                            null != t.explicit && (this.isExplicit = t.explicit),
                            null != t.tage && (this.hT = t.tage,
                            this.isExplicit = !0),
                            null != t.tagi && (this.hT = t.tagi,
                            this.isExplicit = !1),
                            null != t.obj && (t.obj instanceof e.ASN1Object ? (this.asn1Object = t.obj,
                            this.setASN1Object(this.isExplicit, this.hT, this.asn1Object)) : "object" == r(t.obj) && (this.asn1Object = e.ASN1Util.newObject(t.obj),
                            this.setASN1Object(this.isExplicit, this.hT, this.asn1Object)))
                        }
                        ,
                        null != t && this.setByParam(t)
                    }
                    ,
                    o.lang.extend(ct.asn1.DERTaggedObject, ct.asn1.ASN1Object);
                    var ct, ht, lt, ft = new function() {}
                    ;
                    function gt(t) {
                        for (var e = new Array, r = 0; r < t.length; r++)
                            e[r] = t.charCodeAt(r);
                        return e
                    }
                    function dt(t) {
                        for (var e = "", r = 0; r < t.length; r++)
                            e += String.fromCharCode(t[r]);
                        return e
                    }
                    function pt(t) {
                        for (var e = "", r = 0; r < t.length; r++) {
                            var n = t[r].toString(16);
                            1 == n.length && (n = "0" + n),
                            e += n
                        }
                        return e
                    }
                    function vt(t) {
                        return pt(gt(t))
                    }
                    function yt(t) {
                        return (t = (t = t.replace(/\=/g, "")).replace(/\+/g, "-")).replace(/\//g, "_")
                    }
                    function mt(t) {
                        return t.length % 4 == 2 ? t += "==" : t.length % 4 == 3 && (t += "="),
                        (t = t.replace(/-/g, "+")).replace(/_/g, "/")
                    }
                    function _t(t) {
                        return t.length % 2 == 1 && (t = "0" + t),
                        yt(S(t))
                    }
                    function St(t) {
                        return w(mt(t))
                    }
                    function wt(t) {
                        return It(Ot(t))
                    }
                    function bt(t) {
                        return decodeURIComponent(Dt(t))
                    }
                    function Ft(t) {
                        for (var e = "", r = 0; r < t.length - 1; r += 2)
                            e += String.fromCharCode(parseInt(t.substr(r, 2), 16));
                        return e
                    }
                    function Et(t) {
                        for (var e = "", r = 0; r < t.length; r++)
                            e += ("0" + t.charCodeAt(r).toString(16)).slice(-2);
                        return e
                    }
                    function xt(t) {
                        return S(t)
                    }
                    function At(t) {
                        return xt(t).replace(/(.{64})/g, "$1\r\n").replace(/\r\n$/, "")
                    }
                    function kt(t) {
                        return w(t.replace(/[^0-9A-Za-z\/+=]*/g, ""))
                    }
                    function Pt(t, e) {
                        return "-----BEGIN " + e + "-----\r\n" + At(t) + "\r\n-----END " + e + "-----\r\n"
                    }
                    function Ct(t, e) {
                        if (-1 == t.indexOf("-----BEGIN "))
                            throw "can't find PEM header: " + e;
                        return kt(t = void 0 !== e ? (t = t.replace(new RegExp("^[^]*-----BEGIN " + e + "-----"), "")).replace(new RegExp("-----END " + e + "-----[^]*$"), "") : (t = t.replace(/^[^]*-----BEGIN [^-]+-----/, "")).replace(/-----END [^-]+-----[^]*$/, ""))
                    }
                    function Tt(t) {
                        var e, r, n, i, o, s, a, u, c, h, l;
                        if (l = t.match(/^(\d{2}|\d{4})(\d\d)(\d\d)(\d\d)(\d\d)(\d\d)(|\.\d+)Z$/))
                            return u = l[1],
                            e = parseInt(u),
                            2 === u.length && (50 <= e && e < 100 ? e = 1900 + e : 0 <= e && e < 50 && (e = 2e3 + e)),
                            r = parseInt(l[2]) - 1,
                            n = parseInt(l[3]),
                            i = parseInt(l[4]),
                            o = parseInt(l[5]),
                            s = parseInt(l[6]),
                            a = 0,
                            "" !== (c = l[7]) && (h = (c.substr(1) + "00").substr(0, 3),
                            a = parseInt(h)),
                            Date.UTC(e, r, n, i, o, s, a);
                        throw "unsupported zulu format: " + t
                    }
                    function Rt(t) {
                        return ~~(Tt(t) / 1e3)
                    }
                    function It(t) {
                        return t.replace(/%/g, "")
                    }
                    function Dt(t) {
                        return t.replace(/(..)/g, "%$1")
                    }
                    function Lt(t) {
                        var e = "malformed IPv6 address";
                        if (!t.match(/^[0-9A-Fa-f:]+$/))
                            throw e;
                        var r = (t = t.toLowerCase()).split(":").length - 1;
                        if (r < 2)
                            throw e;
                        var n = ":".repeat(7 - r + 2)
                          , i = (t = t.replace("::", n)).split(":");
                        if (8 != i.length)
                            throw e;
                        for (var o = 0; o < 8; o++)
                            i[o] = ("0000" + i[o]).slice(-4);
                        return i.join("")
                    }
                    function Nt(t) {
                        if (!t.match(/^[0-9A-Fa-f]{32}$/))
                            throw "malformed IPv6 address octet";
                        for (var e = (t = t.toLowerCase()).match(/.{1,4}/g), r = 0; r < 8; r++)
                            e[r] = e[r].replace(/^0+/, ""),
                            "" == e[r] && (e[r] = "0");
                        var n = (t = ":" + e.join(":") + ":").match(/:(0:){2,}/g);
                        if (null === n)
                            return t.slice(1, -1);
                        var i = "";
                        for (r = 0; r < n.length; r++)
                            n[r].length > i.length && (i = n[r]);
                        return (t = t.replace(i, "::")).slice(1, -1)
                    }
                    function Ut(t) {
                        var e = "malformed hex value";
                        if (!t.match(/^([0-9A-Fa-f][0-9A-Fa-f]){1,}$/))
                            throw e;
                        if (8 != t.length)
                            return 32 == t.length ? Nt(t) : t;
                        try {
                            return parseInt(t.substr(0, 2), 16) + "." + parseInt(t.substr(2, 2), 16) + "." + parseInt(t.substr(4, 2), 16) + "." + parseInt(t.substr(6, 2), 16)
                        } catch (t) {
                            throw e
                        }
                    }
                    function Ot(t) {
                        for (var e = encodeURIComponent(t), r = "", n = 0; n < e.length; n++)
                            "%" == e[n] ? (r += e.substr(n, 3),
                            n += 2) : r = r + "%" + vt(e[n]);
                        return r
                    }
                    function Bt(t) {
                        return !(t.length % 2 != 0 || !t.match(/^[0-9a-f]+$/) && !t.match(/^[0-9A-F]+$/))
                    }
                    function Mt(t) {
                        return t.length % 2 == 1 ? "0" + t : t.substr(0, 1) > "7" ? "00" + t : t
                    }
                    ft.getLblen = function(t, e) {
                        if ("8" != t.substr(e + 2, 1))
                            return 1;
                        var r = parseInt(t.substr(e + 3, 1));
                        return 0 == r ? -1 : 0 < r && r < 10 ? r + 1 : -2
                    }
                    ,
                    ft.getL = function(t, e) {
                        var r = ft.getLblen(t, e);
                        return r < 1 ? "" : t.substr(e + 2, 2 * r)
                    }
                    ,
                    ft.getVblen = function(t, e) {
                        var r;
                        return "" == (r = ft.getL(t, e)) ? -1 : ("8" === r.substr(0, 1) ? new F(r.substr(2),16) : new F(r,16)).intValue()
                    }
                    ,
                    ft.getVidx = function(t, e) {
                        var r = ft.getLblen(t, e);
                        return r < 0 ? r : e + 2 * (r + 1)
                    }
                    ,
                    ft.getV = function(t, e) {
                        var r = ft.getVidx(t, e)
                          , n = ft.getVblen(t, e);
                        return t.substr(r, 2 * n)
                    }
                    ,
                    ft.getTLV = function(t, e) {
                        return t.substr(e, 2) + ft.getL(t, e) + ft.getV(t, e)
                    }
                    ,
                    ft.getTLVblen = function(t, e) {
                        return 2 + 2 * ft.getLblen(t, e) + 2 * ft.getVblen(t, e)
                    }
                    ,
                    ft.getNextSiblingIdx = function(t, e) {
                        return ft.getVidx(t, e) + 2 * ft.getVblen(t, e)
                    }
                    ,
                    ft.getChildIdx = function(t, e) {
                        var r, n, i, o = ft, s = [];
                        r = o.getVidx(t, e),
                        n = 2 * o.getVblen(t, e),
                        "03" == t.substr(e, 2) && (r += 2,
                        n -= 2),
                        i = 0;
                        for (var a = r; i <= n; ) {
                            var u = o.getTLVblen(t, a);
                            if ((i += u) <= n && s.push(a),
                            a += u,
                            i >= n)
                                break
                        }
                        return s
                    }
                    ,
                    ft.getNthChildIdx = function(t, e, r) {
                        return ft.getChildIdx(t, e)[r]
                    }
                    ,
                    ft.getIdxbyList = function(t, e, r, n) {
                        var i, o, s = ft;
                        return 0 == r.length ? void 0 !== n && t.substr(e, 2) !== n ? -1 : e : (i = r.shift()) >= (o = s.getChildIdx(t, e)).length ? -1 : s.getIdxbyList(t, o[i], r, n)
                    }
                    ,
                    ft.getIdxbyListEx = function(t, e, r, n) {
                        var i, o, s = ft;
                        if (0 == r.length)
                            return void 0 !== n && t.substr(e, 2) !== n ? -1 : e;
                        i = r.shift(),
                        o = s.getChildIdx(t, e);
                        for (var a = 0, u = 0; u < o.length; u++) {
                            var c = t.substr(o[u], 2);
                            if ("number" == typeof i && !s.isContextTag(c) && a == i || "string" == typeof i && s.isContextTag(c, i))
                                return s.getIdxbyListEx(t, o[u], r, n);
                            s.isContextTag(c) || a++
                        }
                        return -1
                    }
                    ,
                    ft.getTLVbyList = function(t, e, r, n) {
                        var i = ft
                          , o = i.getIdxbyList(t, e, r, n);
                        return -1 == o || o >= t.length ? null : i.getTLV(t, o)
                    }
                    ,
                    ft.getTLVbyListEx = function(t, e, r, n) {
                        var i = ft
                          , o = i.getIdxbyListEx(t, e, r, n);
                        return -1 == o ? null : i.getTLV(t, o)
                    }
                    ,
                    ft.getVbyList = function(t, e, r, n, i) {
                        var o, s, a = ft;
                        return -1 == (o = a.getIdxbyList(t, e, r, n)) || o >= t.length ? null : (s = a.getV(t, o),
                        !0 === i && (s = s.substr(2)),
                        s)
                    }
                    ,
                    ft.getVbyListEx = function(t, e, r, n, i) {
                        var o, s, a = ft;
                        return -1 == (o = a.getIdxbyListEx(t, e, r, n)) ? null : (s = a.getV(t, o),
                        "03" == t.substr(o, 2) && !1 !== i && (s = s.substr(2)),
                        s)
                    }
                    ,
                    ft.getInt = function(t, e, r) {
                        null == r && (r = -1);
                        try {
                            var n = t.substr(e, 2);
                            if ("02" != n && "03" != n)
                                return r;
                            var i = ft.getV(t, e);
                            return "02" == n ? parseInt(i, 16) : function(t) {
                                try {
                                    var e = t.substr(0, 2);
                                    if ("00" == e)
                                        return parseInt(t.substr(2), 16);
                                    var r = parseInt(e, 16)
                                      , n = t.substr(2)
                                      , i = parseInt(n, 16).toString(2);
                                    return "0" == i && (i = "00000000"),
                                    i = i.slice(0, 0 - r),
                                    parseInt(i, 2)
                                } catch (t) {
                                    return -1
                                }
                            }(i)
                        } catch (t) {
                            return r
                        }
                    }
                    ,
                    ft.getOID = function(t, e, r) {
                        null == r && (r = null);
                        try {
                            return "06" != t.substr(e, 2) ? r : function(t) {
                                if (!Bt(t))
                                    return null;
                                try {
                                    var e = []
                                      , r = t.substr(0, 2)
                                      , n = parseInt(r, 16);
                                    e[0] = new String(Math.floor(n / 40)),
                                    e[1] = new String(n % 40);
                                    for (var i = t.substr(2), o = [], s = 0; s < i.length / 2; s++)
                                        o.push(parseInt(i.substr(2 * s, 2), 16));
                                    var a = []
                                      , u = "";
                                    for (s = 0; s < o.length; s++)
                                        128 & o[s] ? u += jt((127 & o[s]).toString(2), 7) : (u += jt((127 & o[s]).toString(2), 7),
                                        a.push(new String(parseInt(u, 2))),
                                        u = "");
                                    var c = e.join(".");
                                    return a.length > 0 && (c = c + "." + a.join(".")),
                                    c
                                } catch (t) {
                                    return null
                                }
                            }(ft.getV(t, e))
                        } catch (t) {
                            return r
                        }
                    }
                    ,
                    ft.getOIDName = function(t, e, r) {
                        null == r && (r = null);
                        try {
                            var n = ft.getOID(t, e, r);
                            if (n == r)
                                return r;
                            var i = ct.asn1.x509.OID.oid2name(n);
                            return "" == i ? n : i
                        } catch (t) {
                            return r
                        }
                    }
                    ,
                    ft.getString = function(t, e, r) {
                        null == r && (r = null);
                        try {
                            return Ft(ft.getV(t, e))
                        } catch (t) {
                            return r
                        }
                    }
                    ,
                    ft.hextooidstr = function(t) {
                        var e = function(t, e) {
                            return t.length >= e ? t : new Array(e - t.length + 1).join("0") + t
                        }
                          , r = []
                          , n = t.substr(0, 2)
                          , i = parseInt(n, 16);
                        r[0] = new String(Math.floor(i / 40)),
                        r[1] = new String(i % 40);
                        for (var o = t.substr(2), s = [], a = 0; a < o.length / 2; a++)
                            s.push(parseInt(o.substr(2 * a, 2), 16));
                        var u = []
                          , c = "";
                        for (a = 0; a < s.length; a++)
                            128 & s[a] ? c += e((127 & s[a]).toString(2), 7) : (c += e((127 & s[a]).toString(2), 7),
                            u.push(new String(parseInt(c, 2))),
                            c = "");
                        var h = r.join(".");
                        return u.length > 0 && (h = h + "." + u.join(".")),
                        h
                    }
                    ,
                    ft.dump = function(t, e, r, n) {
                        var i = ft
                          , o = i.getV
                          , s = i.dump
                          , a = i.getChildIdx
                          , u = t;
                        t instanceof ct.asn1.ASN1Object && (u = t.getEncodedHex());
                        var c = function(t, e) {
                            return t.length <= 2 * e ? t : t.substr(0, e) + "..(total " + t.length / 2 + "bytes).." + t.substr(t.length - e, e)
                        };
                        void 0 === e && (e = {
                            ommit_long_octet: 32
                        }),
                        void 0 === r && (r = 0),
                        void 0 === n && (n = "");
                        var h, l = e.ommit_long_octet;
                        if ("01" == (h = u.substr(r, 2)))
                            return "00" == (f = o(u, r)) ? n + "BOOLEAN FALSE\n" : n + "BOOLEAN TRUE\n";
                        if ("02" == h)
                            return n + "INTEGER " + c(f = o(u, r), l) + "\n";
                        if ("03" == h) {
                            var f = o(u, r);
                            return i.isASN1HEX(f.substr(2)) ? (w = n + "BITSTRING, encapsulates\n") + s(f.substr(2), e, 0, n + "  ") : n + "BITSTRING " + c(f, l) + "\n"
                        }
                        if ("04" == h)
                            return f = o(u, r),
                            i.isASN1HEX(f) ? (w = n + "OCTETSTRING, encapsulates\n") + s(f, e, 0, n + "  ") : n + "OCTETSTRING " + c(f, l) + "\n";
                        if ("05" == h)
                            return n + "NULL\n";
                        if ("06" == h) {
                            var g = o(u, r)
                              , d = ct.asn1.ASN1Util.oidHexToInt(g)
                              , p = ct.asn1.x509.OID.oid2name(d)
                              , v = d.replace(/\./g, " ");
                            return "" != p ? n + "ObjectIdentifier " + p + " (" + v + ")\n" : n + "ObjectIdentifier (" + v + ")\n"
                        }
                        if ("0a" == h)
                            return n + "ENUMERATED " + parseInt(o(u, r)) + "\n";
                        if ("0c" == h)
                            return n + "UTF8String '" + bt(o(u, r)) + "'\n";
                        if ("13" == h)
                            return n + "PrintableString '" + bt(o(u, r)) + "'\n";
                        if ("14" == h)
                            return n + "TeletexString '" + bt(o(u, r)) + "'\n";
                        if ("16" == h)
                            return n + "IA5String '" + bt(o(u, r)) + "'\n";
                        if ("17" == h)
                            return n + "UTCTime " + bt(o(u, r)) + "\n";
                        if ("18" == h)
                            return n + "GeneralizedTime " + bt(o(u, r)) + "\n";
                        if ("1a" == h)
                            return n + "VisualString '" + bt(o(u, r)) + "'\n";
                        if ("1e" == h)
                            return n + "BMPString '" + bt(o(u, r)) + "'\n";
                        if ("30" == h) {
                            if ("3000" == u.substr(r, 4))
                                return n + "SEQUENCE {}\n";
                            w = n + "SEQUENCE\n";
                            var y = e;
                            if ((2 == (S = a(u, r)).length || 3 == S.length) && "06" == u.substr(S[0], 2) && "04" == u.substr(S[S.length - 1], 2)) {
                                p = i.oidname(o(u, S[0]));
                                var m = JSON.parse(JSON.stringify(e));
                                m.x509ExtName = p,
                                y = m
                            }
                            for (var _ = 0; _ < S.length; _++)
                                w += s(u, y, S[_], n + "  ");
                            return w
                        }
                        if ("31" == h) {
                            w = n + "SET\n";
                            var S = a(u, r);
                            for (_ = 0; _ < S.length; _++)
                                w += s(u, e, S[_], n + "  ");
                            return w
                        }
                        if (0 != (128 & (h = parseInt(h, 16)))) {
                            var w, b = 31 & h;
                            if (0 != (32 & h)) {
                                for (w = n + "[" + b + "]\n",
                                S = a(u, r),
                                _ = 0; _ < S.length; _++)
                                    w += s(u, e, S[_], n + "  ");
                                return w
                            }
                            return f = o(u, r),
                            ft.isASN1HEX(f) ? (w = n + "[" + b + "]\n") + s(f, e, 0, n + "  ") : (("68747470" == f.substr(0, 8) || "subjectAltName" === e.x509ExtName && 2 == b) && (f = bt(f)),
                            n + "[" + b + "] " + f + "\n")
                        }
                        return n + "UNKNOWN(" + h + ") " + o(u, r) + "\n"
                    }
                    ,
                    ft.isContextTag = function(t, e) {
                        var r, n;
                        t = t.toLowerCase();
                        try {
                            r = parseInt(t, 16)
                        } catch (t) {
                            return -1
                        }
                        if (void 0 === e)
                            return 128 == (192 & r);
                        try {
                            return null != e.match(/^\[[0-9]+\]$/) && !((n = parseInt(e.substr(1, e.length - 1), 10)) > 31) && 128 == (192 & r) && (31 & r) == n
                        } catch (t) {
                            return !1
                        }
                    }
                    ,
                    ft.isASN1HEX = function(t) {
                        var e = ft;
                        if (t.length % 2 == 1)
                            return !1;
                        var r = e.getVblen(t, 0)
                          , n = t.substr(0, 2)
                          , i = e.getL(t, 0);
                        return t.length - n.length - i.length == 2 * r
                    }
                    ,
                    ft.checkStrictDER = function(t, e, r, n, i) {
                        var o = ft;
                        if (void 0 === r) {
                            if ("string" != typeof t)
                                throw new Error("not hex string");
                            if (t = t.toLowerCase(),
                            !ct.lang.String.isHex(t))
                                throw new Error("not hex string");
                            r = t.length,
                            i = (n = t.length / 2) < 128 ? 1 : Math.ceil(n.toString(16)) + 1
                        }
                        if (o.getL(t, e).length > 2 * i)
                            throw new Error("L of TLV too long: idx=" + e);
                        var s = o.getVblen(t, e);
                        if (s > n)
                            throw new Error("value of L too long than hex: idx=" + e);
                        var a = o.getTLV(t, e)
                          , u = a.length - 2 - o.getL(t, e).length;
                        if (u !== 2 * s)
                            throw new Error("V string length and L's value not the same:" + u + "/" + 2 * s);
                        if (0 === e && t.length != a.length)
                            throw new Error("total length and TLV length unmatch:" + t.length + "!=" + a.length);
                        var c = t.substr(e, 2);
                        if ("02" === c) {
                            var h = o.getVidx(t, e);
                            if ("00" == t.substr(h, 2) && t.charCodeAt(h + 2) < 56)
                                throw new Error("not least zeros for DER INTEGER")
                        }
                        if (32 & parseInt(c, 16)) {
                            for (var l = o.getVblen(t, e), f = 0, g = o.getChildIdx(t, e), d = 0; d < g.length; d++)
                                f += o.getTLV(t, g[d]).length,
                                o.checkStrictDER(t, g[d], r, n, i);
                            if (2 * l != f)
                                throw new Error("sum of children's TLV length and L unmatch: " + 2 * l + "!=" + f)
                        }
                    }
                    ,
                    ft.oidname = function(t) {
                        var e = ct.asn1;
                        ct.lang.String.isHex(t) && (t = e.ASN1Util.oidHexToInt(t));
                        var r = e.x509.OID.oid2name(t);
                        return "" === r && (r = t),
                        r
                    }
                    ,
                    void 0 !== ct && ct || (e.KJUR = ct = {}),
                    void 0 !== ct.lang && ct.lang || (ct.lang = {}),
                    ct.lang.String = function() {}
                    ,
                    "function" == typeof t ? (e.utf8tob64u = ht = function(e) {
                        return yt(t.from(e, "utf8").toString("base64"))
                    }
                    ,
                    e.b64utoutf8 = lt = function(e) {
                        return t.from(mt(e), "base64").toString("utf8")
                    }
                    ) : (e.utf8tob64u = ht = function(t) {
                        return _t(It(Ot(t)))
                    }
                    ,
                    e.b64utoutf8 = lt = function(t) {
                        return decodeURIComponent(Dt(St(t)))
                    }
                    ),
                    ct.lang.String.isInteger = function(t) {
                        return !!t.match(/^[0-9]+$/) || !!t.match(/^-[0-9]+$/)
                    }
                    ,
                    ct.lang.String.isHex = function(t) {
                        return Bt(t)
                    }
                    ,
                    ct.lang.String.isBase64 = function(t) {
                        return !(!(t = t.replace(/\s+/g, "")).match(/^[0-9A-Za-z+\/]+={0,3}$/) || t.length % 4 != 0)
                    }
                    ,
                    ct.lang.String.isBase64URL = function(t) {
                        return !t.match(/[+/=]/) && (t = mt(t),
                        ct.lang.String.isBase64(t))
                    }
                    ,
                    ct.lang.String.isIntegerArray = function(t) {
                        return !!(t = t.replace(/\s+/g, "")).match(/^\[[0-9,]+\]$/)
                    }
                    ,
                    ct.lang.String.isPrintable = function(t) {
                        return null !== t.match(/^[0-9A-Za-z '()+,-./:=?]*$/)
                    }
                    ,
                    ct.lang.String.isIA5 = function(t) {
                        return null !== t.match(/^[\x20-\x21\x23-\x7f]*$/)
                    }
                    ,
                    ct.lang.String.isMail = function(t) {
                        return null !== t.match(/^[A-Za-z0-9]{1}[A-Za-z0-9_.-]*@{1}[A-Za-z0-9_.-]{1,}\.[A-Za-z0-9]{1,}$/)
                    }
                    ;
                    var jt = function(t, e, r) {
                        return null == r && (r = "0"),
                        t.length >= e ? t : new Array(e - t.length + 1).join(r) + t
                    };
                    void 0 !== ct && ct || (e.KJUR = ct = {}),
                    void 0 !== ct.crypto && ct.crypto || (ct.crypto = {}),
                    ct.crypto.Util = new function() {
                        this.DIGESTINFOHEAD = {
                            sha1: "3021300906052b0e03021a05000414",
                            sha224: "302d300d06096086480165030402040500041c",
                            sha256: "3031300d060960864801650304020105000420",
                            sha384: "3041300d060960864801650304020205000430",
                            sha512: "3051300d060960864801650304020305000440",
                            md2: "3020300c06082a864886f70d020205000410",
                            md5: "3020300c06082a864886f70d020505000410",
                            ripemd160: "3021300906052b2403020105000414"
                        },
                        this.DEFAULTPROVIDER = {
                            md5: "cryptojs",
                            sha1: "cryptojs",
                            sha224: "cryptojs",
                            sha256: "cryptojs",
                            sha384: "cryptojs",
                            sha512: "cryptojs",
                            ripemd160: "cryptojs",
                            hmacmd5: "cryptojs",
                            hmacsha1: "cryptojs",
                            hmacsha224: "cryptojs",
                            hmacsha256: "cryptojs",
                            hmacsha384: "cryptojs",
                            hmacsha512: "cryptojs",
                            hmacripemd160: "cryptojs",
                            MD5withRSA: "cryptojs/jsrsa",
                            SHA1withRSA: "cryptojs/jsrsa",
                            SHA224withRSA: "cryptojs/jsrsa",
                            SHA256withRSA: "cryptojs/jsrsa",
                            SHA384withRSA: "cryptojs/jsrsa",
                            SHA512withRSA: "cryptojs/jsrsa",
                            RIPEMD160withRSA: "cryptojs/jsrsa",
                            MD5withECDSA: "cryptojs/jsrsa",
                            SHA1withECDSA: "cryptojs/jsrsa",
                            SHA224withECDSA: "cryptojs/jsrsa",
                            SHA256withECDSA: "cryptojs/jsrsa",
                            SHA384withECDSA: "cryptojs/jsrsa",
                            SHA512withECDSA: "cryptojs/jsrsa",
                            RIPEMD160withECDSA: "cryptojs/jsrsa",
                            SHA1withDSA: "cryptojs/jsrsa",
                            SHA224withDSA: "cryptojs/jsrsa",
                            SHA256withDSA: "cryptojs/jsrsa",
                            MD5withRSAandMGF1: "cryptojs/jsrsa",
                            SHAwithRSAandMGF1: "cryptojs/jsrsa",
                            SHA1withRSAandMGF1: "cryptojs/jsrsa",
                            SHA224withRSAandMGF1: "cryptojs/jsrsa",
                            SHA256withRSAandMGF1: "cryptojs/jsrsa",
                            SHA384withRSAandMGF1: "cryptojs/jsrsa",
                            SHA512withRSAandMGF1: "cryptojs/jsrsa",
                            RIPEMD160withRSAandMGF1: "cryptojs/jsrsa"
                        },
                        this.CRYPTOJSMESSAGEDIGESTNAME = {
                            md5: y.algo.MD5,
                            sha1: y.algo.SHA1,
                            sha224: y.algo.SHA224,
                            sha256: y.algo.SHA256,
                            sha384: y.algo.SHA384,
                            sha512: y.algo.SHA512,
                            ripemd160: y.algo.RIPEMD160
                        },
                        this.getDigestInfoHex = function(t, e) {
                            if (void 0 === this.DIGESTINFOHEAD[e])
                                throw "alg not supported in Util.DIGESTINFOHEAD: " + e;
                            return this.DIGESTINFOHEAD[e] + t
                        }
                        ,
                        this.getPaddedDigestInfoHex = function(t, e, r) {
                            var n = this.getDigestInfoHex(t, e)
                              , i = r / 4;
                            if (n.length + 22 > i)
                                throw "key is too short for SigAlg: keylen=" + r + "," + e;
                            for (var o = "0001", s = "00" + n, a = "", u = i - o.length - s.length, c = 0; c < u; c += 2)
                                a += "ff";
                            return o + a + s
                        }
                        ,
                        this.hashString = function(t, e) {
                            return new ct.crypto.MessageDigest({
                                alg: e
                            }).digestString(t)
                        }
                        ,
                        this.hashHex = function(t, e) {
                            return new ct.crypto.MessageDigest({
                                alg: e
                            }).digestHex(t)
                        }
                        ,
                        this.sha1 = function(t) {
                            return this.hashString(t, "sha1")
                        }
                        ,
                        this.sha256 = function(t) {
                            return this.hashString(t, "sha256")
                        }
                        ,
                        this.sha256Hex = function(t) {
                            return this.hashHex(t, "sha256")
                        }
                        ,
                        this.sha512 = function(t) {
                            return this.hashString(t, "sha512")
                        }
                        ,
                        this.sha512Hex = function(t) {
                            return this.hashHex(t, "sha512")
                        }
                        ,
                        this.isKey = function(t) {
                            return t instanceof it || t instanceof ct.crypto.DSA || t instanceof ct.crypto.ECDSA
                        }
                    }
                    ,
                    ct.crypto.Util.md5 = function(t) {
                        return new ct.crypto.MessageDigest({
                            alg: "md5",
                            prov: "cryptojs"
                        }).digestString(t)
                    }
                    ,
                    ct.crypto.Util.ripemd160 = function(t) {
                        return new ct.crypto.MessageDigest({
                            alg: "ripemd160",
                            prov: "cryptojs"
                        }).digestString(t)
                    }
                    ,
                    ct.crypto.Util.SECURERANDOMGEN = new et,
                    ct.crypto.Util.getRandomHexOfNbytes = function(t) {
                        var e = new Array(t);
                        return ct.crypto.Util.SECURERANDOMGEN.nextBytes(e),
                        pt(e)
                    }
                    ,
                    ct.crypto.Util.getRandomBigIntegerOfNbytes = function(t) {
                        return new F(ct.crypto.Util.getRandomHexOfNbytes(t),16)
                    }
                    ,
                    ct.crypto.Util.getRandomHexOfNbits = function(t) {
                        var e = t % 8
                          , r = new Array((t - e) / 8 + 1);
                        return ct.crypto.Util.SECURERANDOMGEN.nextBytes(r),
                        r[0] = (255 << e & 255 ^ 255) & r[0],
                        pt(r)
                    }
                    ,
                    ct.crypto.Util.getRandomBigIntegerOfNbits = function(t) {
                        return new F(ct.crypto.Util.getRandomHexOfNbits(t),16)
                    }
                    ,
                    ct.crypto.Util.getRandomBigIntegerZeroToMax = function(t) {
                        for (var e = t.bitLength(); ; ) {
                            var r = ct.crypto.Util.getRandomBigIntegerOfNbits(e);
                            if (-1 != t.compareTo(r))
                                return r
                        }
                    }
                    ,
                    ct.crypto.Util.getRandomBigIntegerMinToMax = function(t, e) {
                        var r = t.compareTo(e);
                        if (1 == r)
                            throw "biMin is greater than biMax";
                        if (0 == r)
                            return t;
                        var n = e.subtract(t);
                        return ct.crypto.Util.getRandomBigIntegerZeroToMax(n).add(t)
                    }
                    ,
                    ct.crypto.MessageDigest = function(t) {
                        this.setAlgAndProvider = function(t, e) {
                            if (null !== (t = ct.crypto.MessageDigest.getCanonicalAlgName(t)) && void 0 === e && (e = ct.crypto.Util.DEFAULTPROVIDER[t]),
                            -1 != ":md5:sha1:sha224:sha256:sha384:sha512:ripemd160:".indexOf(t) && "cryptojs" == e) {
                                try {
                                    this.md = ct.crypto.Util.CRYPTOJSMESSAGEDIGESTNAME[t].create()
                                } catch (e) {
                                    throw "setAlgAndProvider hash alg set fail alg=" + t + "/" + e
                                }
                                this.updateString = function(t) {
                                    this.md.update(t)
                                }
                                ,
                                this.updateHex = function(t) {
                                    var e = y.enc.Hex.parse(t);
                                    this.md.update(e)
                                }
                                ,
                                this.digest = function() {
                                    return this.md.finalize().toString(y.enc.Hex)
                                }
                                ,
                                this.digestString = function(t) {
                                    return this.updateString(t),
                                    this.digest()
                                }
                                ,
                                this.digestHex = function(t) {
                                    return this.updateHex(t),
                                    this.digest()
                                }
                            }
                            if (-1 != ":sha256:".indexOf(t) && "sjcl" == e) {
                                try {
                                    this.md = new sjcl.hash.sha256
                                } catch (e) {
                                    throw "setAlgAndProvider hash alg set fail alg=" + t + "/" + e
                                }
                                this.updateString = function(t) {
                                    this.md.update(t)
                                }
                                ,
                                this.updateHex = function(t) {
                                    var e = sjcl.codec.hex.toBits(t);
                                    this.md.update(e)
                                }
                                ,
                                this.digest = function() {
                                    var t = this.md.finalize();
                                    return sjcl.codec.hex.fromBits(t)
                                }
                                ,
                                this.digestString = function(t) {
                                    return this.updateString(t),
                                    this.digest()
                                }
                                ,
                                this.digestHex = function(t) {
                                    return this.updateHex(t),
                                    this.digest()
                                }
                            }
                        }
                        ,
                        this.updateString = function(t) {
                            throw "updateString(str) not supported for this alg/prov: " + this.algName + "/" + this.provName
                        }
                        ,
                        this.updateHex = function(t) {
                            throw "updateHex(hex) not supported for this alg/prov: " + this.algName + "/" + this.provName
                        }
                        ,
                        this.digest = function() {
                            throw "digest() not supported for this alg/prov: " + this.algName + "/" + this.provName
                        }
                        ,
                        this.digestString = function(t) {
                            throw "digestString(str) not supported for this alg/prov: " + this.algName + "/" + this.provName
                        }
                        ,
                        this.digestHex = function(t) {
                            throw "digestHex(hex) not supported for this alg/prov: " + this.algName + "/" + this.provName
                        }
                        ,
                        void 0 !== t && void 0 !== t.alg && (this.algName = t.alg,
                        void 0 === t.prov && (this.provName = ct.crypto.Util.DEFAULTPROVIDER[this.algName]),
                        this.setAlgAndProvider(this.algName, this.provName))
                    }
                    ,
                    ct.crypto.MessageDigest.getCanonicalAlgName = function(t) {
                        return "string" == typeof t && (t = (t = t.toLowerCase()).replace(/-/, "")),
                        t
                    }
                    ,
                    ct.crypto.MessageDigest.getHashLength = function(t) {
                        var e = ct.crypto.MessageDigest
                          , r = e.getCanonicalAlgName(t);
                        if (void 0 === e.HASHLENGTH[r])
                            throw "not supported algorithm: " + t;
                        return e.HASHLENGTH[r]
                    }
                    ,
                    ct.crypto.MessageDigest.HASHLENGTH = {
                        md5: 16,
                        sha1: 20,
                        sha224: 28,
                        sha256: 32,
                        sha384: 48,
                        sha512: 64,
                        ripemd160: 20
                    },
                    ct.crypto.Mac = function(t) {
                        this.setAlgAndProvider = function(t, e) {
                            if (null == (t = t.toLowerCase()) && (t = "hmacsha1"),
                            "hmac" != (t = t.toLowerCase()).substr(0, 4))
                                throw "setAlgAndProvider unsupported HMAC alg: " + t;
                            void 0 === e && (e = ct.crypto.Util.DEFAULTPROVIDER[t]),
                            this.algProv = t + "/" + e;
                            var r = t.substr(4);
                            if (-1 != ":md5:sha1:sha224:sha256:sha384:sha512:ripemd160:".indexOf(r) && "cryptojs" == e) {
                                try {
                                    var n = ct.crypto.Util.CRYPTOJSMESSAGEDIGESTNAME[r];
                                    this.mac = y.algo.HMAC.create(n, this.pass)
                                } catch (t) {
                                    throw "setAlgAndProvider hash alg set fail hashAlg=" + r + "/" + t
                                }
                                this.updateString = function(t) {
                                    this.mac.update(t)
                                }
                                ,
                                this.updateHex = function(t) {
                                    var e = y.enc.Hex.parse(t);
                                    this.mac.update(e)
                                }
                                ,
                                this.doFinal = function() {
                                    return this.mac.finalize().toString(y.enc.Hex)
                                }
                                ,
                                this.doFinalString = function(t) {
                                    return this.updateString(t),
                                    this.doFinal()
                                }
                                ,
                                this.doFinalHex = function(t) {
                                    return this.updateHex(t),
                                    this.doFinal()
                                }
                            }
                        }
                        ,
                        this.updateString = function(t) {
                            throw "updateString(str) not supported for this alg/prov: " + this.algProv
                        }
                        ,
                        this.updateHex = function(t) {
                            throw "updateHex(hex) not supported for this alg/prov: " + this.algProv
                        }
                        ,
                        this.doFinal = function() {
                            throw "digest() not supported for this alg/prov: " + this.algProv
                        }
                        ,
                        this.doFinalString = function(t) {
                            throw "digestString(str) not supported for this alg/prov: " + this.algProv
                        }
                        ,
                        this.doFinalHex = function(t) {
                            throw "digestHex(hex) not supported for this alg/prov: " + this.algProv
                        }
                        ,
                        this.setPassword = function(t) {
                            if ("string" == typeof t) {
                                var e = t;
                                return t.length % 2 != 1 && t.match(/^[0-9A-Fa-f]+$/) || (e = Et(t)),
                                void (this.pass = y.enc.Hex.parse(e))
                            }
                            if ("object" != (void 0 === t ? "undefined" : r(t)))
                                throw "KJUR.crypto.Mac unsupported password type: " + t;
                            if (e = null,
                            void 0 !== t.hex) {
                                if (t.hex.length % 2 != 0 || !t.hex.match(/^[0-9A-Fa-f]+$/))
                                    throw "Mac: wrong hex password: " + t.hex;
                                e = t.hex
                            }
                            if (void 0 !== t.utf8 && (e = wt(t.utf8)),
                            void 0 !== t.rstr && (e = Et(t.rstr)),
                            void 0 !== t.b64 && (e = w(t.b64)),
                            void 0 !== t.b64u && (e = St(t.b64u)),
                            null == e)
                                throw "KJUR.crypto.Mac unsupported password type: " + t;
                            this.pass = y.enc.Hex.parse(e)
                        }
                        ,
                        void 0 !== t && (void 0 !== t.pass && this.setPassword(t.pass),
                        void 0 !== t.alg && (this.algName = t.alg,
                        void 0 === t.prov && (this.provName = ct.crypto.Util.DEFAULTPROVIDER[this.algName]),
                        this.setAlgAndProvider(this.algName, this.provName)))
                    }
                    ,
                    ct.crypto.Signature = function(t) {
                        var e = null;
                        if (this._setAlgNames = function() {
                            var t = this.algName.match(/^(.+)with(.+)$/);
                            t && (this.mdAlgName = t[1].toLowerCase(),
                            this.pubkeyAlgName = t[2].toLowerCase(),
                            "rsaandmgf1" == this.pubkeyAlgName && "sha" == this.mdAlgName && (this.mdAlgName = "sha1"))
                        }
                        ,
                        this._zeroPaddingOfSignature = function(t, e) {
                            for (var r = "", n = e / 4 - t.length, i = 0; i < n; i++)
                                r += "0";
                            return r + t
                        }
                        ,
                        this.setAlgAndProvider = function(t, e) {
                            if (this._setAlgNames(),
                            "cryptojs/jsrsa" != e)
                                throw new Error("provider not supported: " + e);
                            if (-1 != ":md5:sha1:sha224:sha256:sha384:sha512:ripemd160:".indexOf(this.mdAlgName)) {
                                try {
                                    this.md = new ct.crypto.MessageDigest({
                                        alg: this.mdAlgName
                                    })
                                } catch (t) {
                                    throw new Error("setAlgAndProvider hash alg set fail alg=" + this.mdAlgName + "/" + t)
                                }
                                this.init = function(t, e) {
                                    var r = null;
                                    try {
                                        r = void 0 === e ? Ht.getKey(t) : Ht.getKey(t, e)
                                    } catch (t) {
                                        throw "init failed:" + t
                                    }
                                    if (!0 === r.isPrivate)
                                        this.prvKey = r,
                                        this.state = "SIGN";
                                    else {
                                        if (!0 !== r.isPublic)
                                            throw "init failed.:" + r;
                                        this.pubKey = r,
                                        this.state = "VERIFY"
                                    }
                                }
                                ,
                                this.updateString = function(t) {
                                    this.md.updateString(t)
                                }
                                ,
                                this.updateHex = function(t) {
                                    this.md.updateHex(t)
                                }
                                ,
                                this.sign = function() {
                                    if (this.sHashHex = this.md.digest(),
                                    void 0 === this.prvKey && void 0 !== this.ecprvhex && void 0 !== this.eccurvename && void 0 !== ct.crypto.ECDSA && (this.prvKey = new ct.crypto.ECDSA({
                                        curve: this.eccurvename,
                                        prv: this.ecprvhex
                                    })),
                                    this.prvKey instanceof it && "rsaandmgf1" === this.pubkeyAlgName)
                                        this.hSign = this.prvKey.signWithMessageHashPSS(this.sHashHex, this.mdAlgName, this.pssSaltLen);
                                    else if (this.prvKey instanceof it && "rsa" === this.pubkeyAlgName)
                                        this.hSign = this.prvKey.signWithMessageHash(this.sHashHex, this.mdAlgName);
                                    else if (this.prvKey instanceof ct.crypto.ECDSA)
                                        this.hSign = this.prvKey.signWithMessageHash(this.sHashHex);
                                    else {
                                        if (!(this.prvKey instanceof ct.crypto.DSA))
                                            throw "Signature: unsupported private key alg: " + this.pubkeyAlgName;
                                        this.hSign = this.prvKey.signWithMessageHash(this.sHashHex)
                                    }
                                    return this.hSign
                                }
                                ,
                                this.signString = function(t) {
                                    return this.updateString(t),
                                    this.sign()
                                }
                                ,
                                this.signHex = function(t) {
                                    return this.updateHex(t),
                                    this.sign()
                                }
                                ,
                                this.verify = function(t) {
                                    if (this.sHashHex = this.md.digest(),
                                    void 0 === this.pubKey && void 0 !== this.ecpubhex && void 0 !== this.eccurvename && void 0 !== ct.crypto.ECDSA && (this.pubKey = new ct.crypto.ECDSA({
                                        curve: this.eccurvename,
                                        pub: this.ecpubhex
                                    })),
                                    this.pubKey instanceof it && "rsaandmgf1" === this.pubkeyAlgName)
                                        return this.pubKey.verifyWithMessageHashPSS(this.sHashHex, t, this.mdAlgName, this.pssSaltLen);
                                    if (this.pubKey instanceof it && "rsa" === this.pubkeyAlgName)
                                        return this.pubKey.verifyWithMessageHash(this.sHashHex, t);
                                    if (void 0 !== ct.crypto.ECDSA && this.pubKey instanceof ct.crypto.ECDSA)
                                        return this.pubKey.verifyWithMessageHash(this.sHashHex, t);
                                    if (void 0 !== ct.crypto.DSA && this.pubKey instanceof ct.crypto.DSA)
                                        return this.pubKey.verifyWithMessageHash(this.sHashHex, t);
                                    throw "Signature: unsupported public key alg: " + this.pubkeyAlgName
                                }
                            }
                        }
                        ,
                        this.init = function(t, e) {
                            throw "init(key, pass) not supported for this alg:prov=" + this.algProvName
                        }
                        ,
                        this.updateString = function(t) {
                            throw "updateString(str) not supported for this alg:prov=" + this.algProvName
                        }
                        ,
                        this.updateHex = function(t) {
                            throw "updateHex(hex) not supported for this alg:prov=" + this.algProvName
                        }
                        ,
                        this.sign = function() {
                            throw "sign() not supported for this alg:prov=" + this.algProvName
                        }
                        ,
                        this.signString = function(t) {
                            throw "digestString(str) not supported for this alg:prov=" + this.algProvName
                        }
                        ,
                        this.signHex = function(t) {
                            throw "digestHex(hex) not supported for this alg:prov=" + this.algProvName
                        }
                        ,
                        this.verify = function(t) {
                            throw "verify(hSigVal) not supported for this alg:prov=" + this.algProvName
                        }
                        ,
                        this.initParams = t,
                        void 0 !== t && (void 0 !== t.alg && (this.algName = t.alg,
                        void 0 === t.prov ? this.provName = ct.crypto.Util.DEFAULTPROVIDER[this.algName] : this.provName = t.prov,
                        this.algProvName = this.algName + ":" + this.provName,
                        this.setAlgAndProvider(this.algName, this.provName),
                        this._setAlgNames()),
                        void 0 !== t.psssaltlen && (this.pssSaltLen = t.psssaltlen),
                        void 0 !== t.prvkeypem)) {
                            if (void 0 !== t.prvkeypas)
                                throw "both prvkeypem and prvkeypas parameters not supported";
                            try {
                                e = Ht.getKey(t.prvkeypem),
                                this.init(e)
                            } catch (t) {
                                throw "fatal error to load pem private key: " + t
                            }
                        }
                    }
                    ,
                    ct.crypto.Cipher = function(t) {}
                    ,
                    ct.crypto.Cipher.encrypt = function(t, e, r) {
                        if (e instanceof it && e.isPublic) {
                            var n = ct.crypto.Cipher.getAlgByKeyAndName(e, r);
                            if ("RSA" === n)
                                return e.encrypt(t);
                            if ("RSAOAEP" === n)
                                return e.encryptOAEP(t, "sha1");
                            var i = n.match(/^RSAOAEP(\d+)$/);
                            if (null !== i)
                                return e.encryptOAEP(t, "sha" + i[1]);
                            throw "Cipher.encrypt: unsupported algorithm for RSAKey: " + r
                        }
                        throw "Cipher.encrypt: unsupported key or algorithm"
                    }
                    ,
                    ct.crypto.Cipher.decrypt = function(t, e, r) {
                        if (e instanceof it && e.isPrivate) {
                            var n = ct.crypto.Cipher.getAlgByKeyAndName(e, r);
                            if ("RSA" === n)
                                return e.decrypt(t);
                            if ("RSAOAEP" === n)
                                return e.decryptOAEP(t, "sha1");
                            var i = n.match(/^RSAOAEP(\d+)$/);
                            if (null !== i)
                                return e.decryptOAEP(t, "sha" + i[1]);
                            throw "Cipher.decrypt: unsupported algorithm for RSAKey: " + r
                        }
                        throw "Cipher.decrypt: unsupported key or algorithm"
                    }
                    ,
                    ct.crypto.Cipher.getAlgByKeyAndName = function(t, e) {
                        if (t instanceof it) {
                            if (-1 != ":RSA:RSAOAEP:RSAOAEP224:RSAOAEP256:RSAOAEP384:RSAOAEP512:".indexOf(e))
                                return e;
                            if (null == e)
                                return "RSA";
                            throw "getAlgByKeyAndName: not supported algorithm name for RSAKey: " + e
                        }
                        throw "getAlgByKeyAndName: not supported algorithm name: " + e
                    }
                    ,
                    ct.crypto.OID = new function() {
                        this.oidhex2name = {
                            "2a864886f70d010101": "rsaEncryption",
                            "2a8648ce3d0201": "ecPublicKey",
                            "2a8648ce380401": "dsa",
                            "2a8648ce3d030107": "secp256r1",
                            "2b8104001f": "secp192k1",
                            "2b81040021": "secp224r1",
                            "2b8104000a": "secp256k1",
                            "2b81040023": "secp521r1",
                            "2b81040022": "secp384r1",
                            "2a8648ce380403": "SHA1withDSA",
                            "608648016503040301": "SHA224withDSA",
                            "608648016503040302": "SHA256withDSA"
                        }
                    }
                    ,
                    void 0 !== ct && ct || (e.KJUR = ct = {}),
                    void 0 !== ct.crypto && ct.crypto || (ct.crypto = {}),
                    ct.crypto.ECDSA = function(t) {
                        var e = Error
                          , n = F
                          , i = st
                          , o = ct.crypto.ECDSA
                          , s = ct.crypto.ECParameterDB
                          , a = o.getName
                          , u = ft
                          , c = u.getVbyListEx
                          , h = u.isASN1HEX
                          , l = new et;
                        this.type = "EC",
                        this.isPrivate = !1,
                        this.isPublic = !1,
                        this.getBigRandom = function(t) {
                            return new n(t.bitLength(),l).mod(t.subtract(n.ONE)).add(n.ONE)
                        }
                        ,
                        this.setNamedCurve = function(t) {
                            this.ecparams = s.getByName(t),
                            this.prvKeyHex = null,
                            this.pubKeyHex = null,
                            this.curveName = t
                        }
                        ,
                        this.setPrivateKeyHex = function(t) {
                            this.isPrivate = !0,
                            this.prvKeyHex = t
                        }
                        ,
                        this.setPublicKeyHex = function(t) {
                            this.isPublic = !0,
                            this.pubKeyHex = t
                        }
                        ,
                        this.getPublicKeyXYHex = function() {
                            var t = this.pubKeyHex;
                            if ("04" !== t.substr(0, 2))
                                throw "this method supports uncompressed format(04) only";
                            var e = this.ecparams.keylen / 4;
                            if (t.length !== 2 + 2 * e)
                                throw "malformed public key hex length";
                            var r = {};
                            return r.x = t.substr(2, e),
                            r.y = t.substr(2 + e),
                            r
                        }
                        ,
                        this.getShortNISTPCurveName = function() {
                            var t = this.curveName;
                            return "secp256r1" === t || "NIST P-256" === t || "P-256" === t || "prime256v1" === t ? "P-256" : "secp384r1" === t || "NIST P-384" === t || "P-384" === t ? "P-384" : null
                        }
                        ,
                        this.generateKeyPairHex = function() {
                            var t = this.ecparams.n
                              , e = this.getBigRandom(t)
                              , r = this.ecparams.G.multiply(e)
                              , n = r.getX().toBigInteger()
                              , i = r.getY().toBigInteger()
                              , o = this.ecparams.keylen / 4
                              , s = ("0000000000" + e.toString(16)).slice(-o)
                              , a = "04" + ("0000000000" + n.toString(16)).slice(-o) + ("0000000000" + i.toString(16)).slice(-o);
                            return this.setPrivateKeyHex(s),
                            this.setPublicKeyHex(a),
                            {
                                ecprvhex: s,
                                ecpubhex: a
                            }
                        }
                        ,
                        this.signWithMessageHash = function(t) {
                            return this.signHex(t, this.prvKeyHex)
                        }
                        ,
                        this.signHex = function(t, e) {
                            var r = new n(e,16)
                              , i = this.ecparams.n
                              , s = new n(t.substring(0, this.ecparams.keylen / 4),16);
                            do {
                                var a = this.getBigRandom(i)
                                  , u = this.ecparams.G.multiply(a).getX().toBigInteger().mod(i)
                            } while (u.compareTo(n.ZERO) <= 0);
                            var c = a.modInverse(i).multiply(s.add(r.multiply(u))).mod(i);
                            return o.biRSSigToASN1Sig(u, c)
                        }
                        ,
                        this.sign = function(t, e) {
                            var r = e
                              , i = this.ecparams.n
                              , o = n.fromByteArrayUnsigned(t);
                            do {
                                var s = this.getBigRandom(i)
                                  , a = this.ecparams.G.multiply(s).getX().toBigInteger().mod(i)
                            } while (a.compareTo(F.ZERO) <= 0);
                            var u = s.modInverse(i).multiply(o.add(r.multiply(a))).mod(i);
                            return this.serializeSig(a, u)
                        }
                        ,
                        this.verifyWithMessageHash = function(t, e) {
                            return this.verifyHex(t, e, this.pubKeyHex)
                        }
                        ,
                        this.verifyHex = function(t, e, r) {
                            try {
                                var s, a, u = o.parseSigHex(e);
                                s = u.r,
                                a = u.s;
                                var c = i.decodeFromHex(this.ecparams.curve, r)
                                  , h = new n(t.substring(0, this.ecparams.keylen / 4),16);
                                return this.verifyRaw(h, s, a, c)
                            } catch (t) {
                                return !1
                            }
                        }
                        ,
                        this.verify = function(t, e, o) {
                            var s, a, u;
                            if (Bitcoin.Util.isArray(e)) {
                                var c = this.parseSig(e);
                                s = c.r,
                                a = c.s
                            } else {
                                if ("object" !== (void 0 === e ? "undefined" : r(e)) || !e.r || !e.s)
                                    throw "Invalid value for signature";
                                s = e.r,
                                a = e.s
                            }
                            if (o instanceof st)
                                u = o;
                            else {
                                if (!Bitcoin.Util.isArray(o))
                                    throw "Invalid format for pubkey value, must be byte array or ECPointFp";
                                u = i.decodeFrom(this.ecparams.curve, o)
                            }
                            var h = n.fromByteArrayUnsigned(t);
                            return this.verifyRaw(h, s, a, u)
                        }
                        ,
                        this.verifyRaw = function(t, e, r, i) {
                            var o = this.ecparams.n
                              , s = this.ecparams.G;
                            if (e.compareTo(n.ONE) < 0 || e.compareTo(o) >= 0)
                                return !1;
                            if (r.compareTo(n.ONE) < 0 || r.compareTo(o) >= 0)
                                return !1;
                            var a = r.modInverse(o)
                              , u = t.multiply(a).mod(o)
                              , c = e.multiply(a).mod(o);
                            return s.multiply(u).add(i.multiply(c)).getX().toBigInteger().mod(o).equals(e)
                        }
                        ,
                        this.serializeSig = function(t, e) {
                            var r = t.toByteArraySigned()
                              , n = e.toByteArraySigned()
                              , i = [];
                            return i.push(2),
                            i.push(r.length),
                            (i = i.concat(r)).push(2),
                            i.push(n.length),
                            (i = i.concat(n)).unshift(i.length),
                            i.unshift(48),
                            i
                        }
                        ,
                        this.parseSig = function(t) {
                            var e;
                            if (48 != t[0])
                                throw new Error("Signature not a valid DERSequence");
                            if (2 != t[e = 2])
                                throw new Error("First element in signature must be a DERInteger");
                            var r = t.slice(e + 2, e + 2 + t[e + 1]);
                            if (2 != t[e += 2 + t[e + 1]])
                                throw new Error("Second element in signature must be a DERInteger");
                            var i = t.slice(e + 2, e + 2 + t[e + 1]);
                            return e += 2 + t[e + 1],
                            {
                                r: n.fromByteArrayUnsigned(r),
                                s: n.fromByteArrayUnsigned(i)
                            }
                        }
                        ,
                        this.parseSigCompact = function(t) {
                            if (65 !== t.length)
                                throw "Signature has the wrong length";
                            var e = t[0] - 27;
                            if (e < 0 || e > 7)
                                throw "Invalid signature type";
                            var r = this.ecparams.n;
                            return {
                                r: n.fromByteArrayUnsigned(t.slice(1, 33)).mod(r),
                                s: n.fromByteArrayUnsigned(t.slice(33, 65)).mod(r),
                                i: e
                            }
                        }
                        ,
                        this.readPKCS5PrvKeyHex = function(t) {
                            if (!1 === h(t))
                                throw new Error("not ASN.1 hex string");
                            var e, r, n;
                            try {
                                e = c(t, 0, ["[0]", 0], "06"),
                                r = c(t, 0, [1], "04");
                                try {
                                    n = c(t, 0, ["[1]", 0], "03")
                                } catch (t) {}
                            } catch (t) {
                                throw new Error("malformed PKCS#1/5 plain ECC private key")
                            }
                            if (this.curveName = a(e),
                            void 0 === this.curveName)
                                throw "unsupported curve name";
                            this.setNamedCurve(this.curveName),
                            this.setPublicKeyHex(n),
                            this.setPrivateKeyHex(r),
                            this.isPublic = !1
                        }
                        ,
                        this.readPKCS8PrvKeyHex = function(t) {
                            if (!1 === h(t))
                                throw new e("not ASN.1 hex string");
                            var r, n, i;
                            try {
                                c(t, 0, [1, 0], "06"),
                                r = c(t, 0, [1, 1], "06"),
                                n = c(t, 0, [2, 0, 1], "04");
                                try {
                                    i = c(t, 0, [2, 0, "[1]", 0], "03")
                                } catch (t) {}
                            } catch (t) {
                                throw new e("malformed PKCS#8 plain ECC private key")
                            }
                            if (this.curveName = a(r),
                            void 0 === this.curveName)
                                throw new e("unsupported curve name");
                            this.setNamedCurve(this.curveName),
                            this.setPublicKeyHex(i),
                            this.setPrivateKeyHex(n),
                            this.isPublic = !1
                        }
                        ,
                        this.readPKCS8PubKeyHex = function(t) {
                            if (!1 === h(t))
                                throw new e("not ASN.1 hex string");
                            var r, n;
                            try {
                                c(t, 0, [0, 0], "06"),
                                r = c(t, 0, [0, 1], "06"),
                                n = c(t, 0, [1], "03")
                            } catch (t) {
                                throw new e("malformed PKCS#8 ECC public key")
                            }
                            if (this.curveName = a(r),
                            null === this.curveName)
                                throw new e("unsupported curve name");
                            this.setNamedCurve(this.curveName),
                            this.setPublicKeyHex(n)
                        }
                        ,
                        this.readCertPubKeyHex = function(t, r) {
                            if (!1 === h(t))
                                throw new e("not ASN.1 hex string");
                            var n, i;
                            try {
                                n = c(t, 0, [0, 5, 0, 1], "06"),
                                i = c(t, 0, [0, 5, 1], "03")
                            } catch (t) {
                                throw new e("malformed X.509 certificate ECC public key")
                            }
                            if (this.curveName = a(n),
                            null === this.curveName)
                                throw new e("unsupported curve name");
                            this.setNamedCurve(this.curveName),
                            this.setPublicKeyHex(i)
                        }
                        ,
                        void 0 !== t && void 0 !== t.curve && (this.curveName = t.curve),
                        void 0 === this.curveName && (this.curveName = "secp256r1"),
                        this.setNamedCurve(this.curveName),
                        void 0 !== t && (void 0 !== t.prv && this.setPrivateKeyHex(t.prv),
                        void 0 !== t.pub && this.setPublicKeyHex(t.pub))
                    }
                    ,
                    ct.crypto.ECDSA.parseSigHex = function(t) {
                        var e = ct.crypto.ECDSA.parseSigHexInHexRS(t);
                        return {
                            r: new F(e.r,16),
                            s: new F(e.s,16)
                        }
                    }
                    ,
                    ct.crypto.ECDSA.parseSigHexInHexRS = function(t) {
                        var e = ft
                          , r = e.getChildIdx
                          , n = e.getV;
                        if (e.checkStrictDER(t, 0),
                        "30" != t.substr(0, 2))
                            throw new Error("signature is not a ASN.1 sequence");
                        var i = r(t, 0);
                        if (2 != i.length)
                            throw new Error("signature shall have two elements");
                        var o = i[0]
                          , s = i[1];
                        if ("02" != t.substr(o, 2))
                            throw new Error("1st item not ASN.1 integer");
                        if ("02" != t.substr(s, 2))
                            throw new Error("2nd item not ASN.1 integer");
                        return {
                            r: n(t, o),
                            s: n(t, s)
                        }
                    }
                    ,
                    ct.crypto.ECDSA.asn1SigToConcatSig = function(t) {
                        var e = ct.crypto.ECDSA.parseSigHexInHexRS(t)
                          , r = e.r
                          , n = e.s;
                        if ("00" == r.substr(0, 2) && r.length % 32 == 2 && (r = r.substr(2)),
                        "00" == n.substr(0, 2) && n.length % 32 == 2 && (n = n.substr(2)),
                        r.length % 32 == 30 && (r = "00" + r),
                        n.length % 32 == 30 && (n = "00" + n),
                        r.length % 32 != 0)
                            throw "unknown ECDSA sig r length error";
                        if (n.length % 32 != 0)
                            throw "unknown ECDSA sig s length error";
                        return r + n
                    }
                    ,
                    ct.crypto.ECDSA.concatSigToASN1Sig = function(t) {
                        if (t.length / 2 * 8 % 128 != 0)
                            throw "unknown ECDSA concatinated r-s sig  length error";
                        var e = t.substr(0, t.length / 2)
                          , r = t.substr(t.length / 2);
                        return ct.crypto.ECDSA.hexRSSigToASN1Sig(e, r)
                    }
                    ,
                    ct.crypto.ECDSA.hexRSSigToASN1Sig = function(t, e) {
                        var r = new F(t,16)
                          , n = new F(e,16);
                        return ct.crypto.ECDSA.biRSSigToASN1Sig(r, n)
                    }
                    ,
                    ct.crypto.ECDSA.biRSSigToASN1Sig = function(t, e) {
                        var r = ct.asn1
                          , n = new r.DERInteger({
                            bigint: t
                        })
                          , i = new r.DERInteger({
                            bigint: e
                        });
                        return new r.DERSequence({
                            array: [n, i]
                        }).getEncodedHex()
                    }
                    ,
                    ct.crypto.ECDSA.getName = function(t) {
                        return "2b8104001f" === t ? "secp192k1" : "2a8648ce3d030107" === t ? "secp256r1" : "2b8104000a" === t ? "secp256k1" : "2b81040021" === t ? "secp224r1" : "2b81040022" === t ? "secp384r1" : -1 !== "|secp256r1|NIST P-256|P-256|prime256v1|".indexOf(t) ? "secp256r1" : -1 !== "|secp256k1|".indexOf(t) ? "secp256k1" : -1 !== "|secp224r1|NIST P-224|P-224|".indexOf(t) ? "secp224r1" : -1 !== "|secp384r1|NIST P-384|P-384|".indexOf(t) ? "secp384r1" : null
                    }
                    ,
                    void 0 !== ct && ct || (e.KJUR = ct = {}),
                    void 0 !== ct.crypto && ct.crypto || (ct.crypto = {}),
                    ct.crypto.ECParameterDB = new function() {
                        var t = {}
                          , e = {};
                        function r(t) {
                            return new F(t,16)
                        }
                        this.getByName = function(r) {
                            var n = r;
                            if (void 0 !== e[n] && (n = e[r]),
                            void 0 !== t[n])
                                return t[n];
                            throw "unregistered EC curve name: " + n
                        }
                        ,
                        this.regist = function(n, i, o, s, a, u, c, h, l, f, g, d) {
                            t[n] = {};
                            var p = r(o)
                              , v = r(s)
                              , y = r(a)
                              , m = r(u)
                              , _ = r(c)
                              , S = new at(p,v,y)
                              , w = S.decodePointHex("04" + h + l);
                            t[n].name = n,
                            t[n].keylen = i,
                            t[n].curve = S,
                            t[n].G = w,
                            t[n].n = m,
                            t[n].h = _,
                            t[n].oid = g,
                            t[n].info = d;
                            for (var b = 0; b < f.length; b++)
                                e[f[b]] = n
                        }
                    }
                    ,
                    ct.crypto.ECParameterDB.regist("secp128r1", 128, "FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFF", "FFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFC", "E87579C11079F43DD824993C2CEE5ED3", "FFFFFFFE0000000075A30D1B9038A115", "1", "161FF7528B899B2D0C28607CA52C5B86", "CF5AC8395BAFEB13C02DA292DDED7A83", [], "", "secp128r1 : SECG curve over a 128 bit prime field"),
                    ct.crypto.ECParameterDB.regist("secp160k1", 160, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFAC73", "0", "7", "0100000000000000000001B8FA16DFAB9ACA16B6B3", "1", "3B4C382CE37AA192A4019E763036F4F5DD4D7EBB", "938CF935318FDCED6BC28286531733C3F03C4FEE", [], "", "secp160k1 : SECG curve over a 160 bit prime field"),
                    ct.crypto.ECParameterDB.regist("secp160r1", 160, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFF", "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7FFFFFFC", "1C97BEFC54BD7A8B65ACF89F81D4D4ADC565FA45", "0100000000000000000001F4C8F927AED3CA752257", "1", "4A96B5688EF573284664698968C38BB913CBFC82", "23A628553168947D59DCC912042351377AC5FB32", [], "", "secp160r1 : SECG curve over a 160 bit prime field"),
                    ct.crypto.ECParameterDB.regist("secp192k1", 192, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFEE37", "0", "3", "FFFFFFFFFFFFFFFFFFFFFFFE26F2FC170F69466A74DEFD8D", "1", "DB4FF10EC057E9AE26B07D0280B7F4341DA5D1B1EAE06C7D", "9B2F2F6D9C5628A7844163D015BE86344082AA88D95E2F9D", []),
                    ct.crypto.ECParameterDB.regist("secp192r1", 192, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFF", "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFC", "64210519E59C80E70FA7E9AB72243049FEB8DEECC146B9B1", "FFFFFFFFFFFFFFFFFFFFFFFF99DEF836146BC9B1B4D22831", "1", "188DA80EB03090F67CBF20EB43A18800F4FF0AFD82FF1012", "07192B95FFC8DA78631011ED6B24CDD573F977A11E794811", []),
                    ct.crypto.ECParameterDB.regist("secp224r1", 224, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF000000000000000000000001", "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFE", "B4050A850C04B3ABF54132565044B0B7D7BFD8BA270B39432355FFB4", "FFFFFFFFFFFFFFFFFFFFFFFFFFFF16A2E0B8F03E13DD29455C5C2A3D", "1", "B70E0CBD6BB4BF7F321390B94A03C1D356C21122343280D6115C1D21", "BD376388B5F723FB4C22DFE6CD4375A05A07476444D5819985007E34", []),
                    ct.crypto.ECParameterDB.regist("secp256k1", 256, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F", "0", "7", "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141", "1", "79BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798", "483ADA7726A3C4655DA4FBFC0E1108A8FD17B448A68554199C47D08FFB10D4B8", []),
                    ct.crypto.ECParameterDB.regist("secp256r1", 256, "FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFF", "FFFFFFFF00000001000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFC", "5AC635D8AA3A93E7B3EBBD55769886BC651D06B0CC53B0F63BCE3C3E27D2604B", "FFFFFFFF00000000FFFFFFFFFFFFFFFFBCE6FAADA7179E84F3B9CAC2FC632551", "1", "6B17D1F2E12C4247F8BCE6E563A440F277037D812DEB33A0F4A13945D898C296", "4FE342E2FE1A7F9B8EE7EB4A7C0F9E162BCE33576B315ECECBB6406837BF51F5", ["NIST P-256", "P-256", "prime256v1"]),
                    ct.crypto.ECParameterDB.regist("secp384r1", 384, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFF", "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFFFF0000000000000000FFFFFFFC", "B3312FA7E23EE7E4988E056BE3F82D19181D9C6EFE8141120314088F5013875AC656398D8A2ED19D2A85C8EDD3EC2AEF", "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC7634D81F4372DDF581A0DB248B0A77AECEC196ACCC52973", "1", "AA87CA22BE8B05378EB1C71EF320AD746E1D3B628BA79B9859F741E082542A385502F25DBF55296C3A545E3872760AB7", "3617de4a96262c6f5d9e98bf9292dc29f8f41dbd289a147ce9da3113b5f0b8c00a60b1ce1d7e819d7a431d7c90ea0e5f", ["NIST P-384", "P-384"]),
                    ct.crypto.ECParameterDB.regist("secp521r1", 521, "1FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", "1FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC", "051953EB9618E1C9A1F929A21A0B68540EEA2DA725B99B315F3B8B489918EF109E156193951EC7E937B1652C0BD3BB1BF073573DF883D2C34F1EF451FD46B503F00", "1FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFA51868783BF2F966B7FCC0148F709A5D03BB5C9B8899C47AEBB6FB71E91386409", "1", "C6858E06B70404E9CD9E3ECB662395B4429C648139053FB521F828AF606B4D3DBAA14B5E77EFE75928FE1DC127A2FFA8DE3348B3C1856A429BF97E7E31C2E5BD66", "011839296a789a3bc0045c8a5fb42c7d1bd998f54449579b446817afbd17273e662c97ee72995ef42640c550b9013fad0761353c7086a272c24088be94769fd16650", ["NIST P-521", "P-521"]);
                    var Ht = function() {
                        var t = function(t, r, n) {
                            return e(y.AES, t, r, n)
                        }
                          , e = function(t, e, r, n) {
                            var i = y.enc.Hex.parse(e)
                              , o = y.enc.Hex.parse(r)
                              , s = y.enc.Hex.parse(n)
                              , a = {};
                            a.key = o,
                            a.iv = s,
                            a.ciphertext = i;
                            var u = t.decrypt(a, o, {
                                iv: s
                            });
                            return y.enc.Hex.stringify(u)
                        }
                          , r = function(t, e, r) {
                            return n(y.AES, t, e, r)
                        }
                          , n = function(t, e, r, n) {
                            var i = y.enc.Hex.parse(e)
                              , o = y.enc.Hex.parse(r)
                              , s = y.enc.Hex.parse(n)
                              , a = t.encrypt(i, o, {
                                iv: s
                            })
                              , u = y.enc.Hex.parse(a.toString());
                            return y.enc.Base64.stringify(u)
                        }
                          , i = {
                            "AES-256-CBC": {
                                proc: t,
                                eproc: r,
                                keylen: 32,
                                ivlen: 16
                            },
                            "AES-192-CBC": {
                                proc: t,
                                eproc: r,
                                keylen: 24,
                                ivlen: 16
                            },
                            "AES-128-CBC": {
                                proc: t,
                                eproc: r,
                                keylen: 16,
                                ivlen: 16
                            },
                            "DES-EDE3-CBC": {
                                proc: function(t, r, n) {
                                    return e(y.TripleDES, t, r, n)
                                },
                                eproc: function(t, e, r) {
                                    return n(y.TripleDES, t, e, r)
                                },
                                keylen: 24,
                                ivlen: 8
                            },
                            "DES-CBC": {
                                proc: function(t, r, n) {
                                    return e(y.DES, t, r, n)
                                },
                                eproc: function(t, e, r) {
                                    return n(y.DES, t, e, r)
                                },
                                keylen: 8,
                                ivlen: 8
                            }
                        }
                          , o = function(t) {
                            var e = {}
                              , r = t.match(new RegExp("DEK-Info: ([^,]+),([0-9A-Fa-f]+)","m"));
                            r && (e.cipher = r[1],
                            e.ivsalt = r[2]);
                            var n = t.match(new RegExp("-----BEGIN ([A-Z]+) PRIVATE KEY-----"));
                            n && (e.type = n[1]);
                            var i = -1
                              , o = 0;
                            -1 != t.indexOf("\r\n\r\n") && (i = t.indexOf("\r\n\r\n"),
                            o = 2),
                            -1 != t.indexOf("\n\n") && (i = t.indexOf("\n\n"),
                            o = 1);
                            var s = t.indexOf("-----END");
                            if (-1 != i && -1 != s) {
                                var a = t.substring(i + 2 * o, s - o);
                                a = a.replace(/\s+/g, ""),
                                e.data = a
                            }
                            return e
                        }
                          , s = function(t, e, r) {
                            for (var n = r.substring(0, 16), o = y.enc.Hex.parse(n), s = y.enc.Utf8.parse(e), a = i[t].keylen + i[t].ivlen, u = "", c = null; ; ) {
                                var h = y.algo.MD5.create();
                                if (null != c && h.update(c),
                                h.update(s),
                                h.update(o),
                                c = h.finalize(),
                                (u += y.enc.Hex.stringify(c)).length >= 2 * a)
                                    break
                            }
                            var l = {};
                            return l.keyhex = u.substr(0, 2 * i[t].keylen),
                            l.ivhex = u.substr(2 * i[t].keylen, 2 * i[t].ivlen),
                            l
                        }
                          , a = function(t, e, r, n) {
                            var o = y.enc.Base64.parse(t)
                              , s = y.enc.Hex.stringify(o);
                            return (0,
                            i[e].proc)(s, r, n)
                        };
                        return {
                            version: "1.0.0",
                            parsePKCS5PEM: function(t) {
                                return o(t)
                            },
                            getKeyAndUnusedIvByPasscodeAndIvsalt: function(t, e, r) {
                                return s(t, e, r)
                            },
                            decryptKeyB64: function(t, e, r, n) {
                                return a(t, e, r, n)
                            },
                            getDecryptedKeyHex: function(t, e) {
                                var r = o(t)
                                  , n = (r.type,
                                r.cipher)
                                  , i = r.ivsalt
                                  , u = r.data
                                  , c = s(n, e, i).keyhex;
                                return a(u, n, c, i)
                            },
                            getEncryptedPKCS5PEMFromPrvKeyHex: function(t, e, r, n, o) {
                                var a = "";
                                if (void 0 !== n && null != n || (n = "AES-256-CBC"),
                                void 0 === i[n])
                                    throw "KEYUTIL unsupported algorithm: " + n;
                                return void 0 !== o && null != o || (o = function(t) {
                                    var e = y.lib.WordArray.random(t);
                                    return y.enc.Hex.stringify(e)
                                }(i[n].ivlen).toUpperCase()),
                                a = "-----BEGIN " + t + " PRIVATE KEY-----\r\n",
                                a += "Proc-Type: 4,ENCRYPTED\r\n",
                                a += "DEK-Info: " + n + "," + o + "\r\n",
                                a += "\r\n",
                                (a += function(t, e, r, n) {
                                    return (0,
                                    i[e].eproc)(t, r, n)
                                }(e, n, s(n, r, o).keyhex, o).replace(/(.{64})/g, "$1\r\n")) + "\r\n-----END " + t + " PRIVATE KEY-----\r\n"
                            },
                            parseHexOfEncryptedPKCS8: function(t) {
                                var e = ft
                                  , r = e.getChildIdx
                                  , n = e.getV
                                  , i = {}
                                  , o = r(t, 0);
                                if (2 != o.length)
                                    throw "malformed format: SEQUENCE(0).items != 2: " + o.length;
                                i.ciphertext = n(t, o[1]);
                                var s = r(t, o[0]);
                                if (2 != s.length)
                                    throw "malformed format: SEQUENCE(0.0).items != 2: " + s.length;
                                if ("2a864886f70d01050d" != n(t, s[0]))
                                    throw "this only supports pkcs5PBES2";
                                var a = r(t, s[1]);
                                if (2 != s.length)
                                    throw "malformed format: SEQUENCE(0.0.1).items != 2: " + a.length;
                                var u = r(t, a[1]);
                                if (2 != u.length)
                                    throw "malformed format: SEQUENCE(0.0.1.1).items != 2: " + u.length;
                                if ("2a864886f70d0307" != n(t, u[0]))
                                    throw "this only supports TripleDES";
                                i.encryptionSchemeAlg = "TripleDES",
                                i.encryptionSchemeIV = n(t, u[1]);
                                var c = r(t, a[0]);
                                if (2 != c.length)
                                    throw "malformed format: SEQUENCE(0.0.1.0).items != 2: " + c.length;
                                if ("2a864886f70d01050c" != n(t, c[0]))
                                    throw "this only supports pkcs5PBKDF2";
                                var h = r(t, c[1]);
                                if (h.length < 2)
                                    throw "malformed format: SEQUENCE(0.0.1.0.1).items < 2: " + h.length;
                                i.pbkdf2Salt = n(t, h[0]);
                                var l = n(t, h[1]);
                                try {
                                    i.pbkdf2Iter = parseInt(l, 16)
                                } catch (t) {
                                    throw "malformed format pbkdf2Iter: " + l
                                }
                                return i
                            },
                            getPBKDF2KeyHexFromParam: function(t, e) {
                                var r = y.enc.Hex.parse(t.pbkdf2Salt)
                                  , n = t.pbkdf2Iter
                                  , i = y.PBKDF2(e, r, {
                                    keySize: 6,
                                    iterations: n
                                });
                                return y.enc.Hex.stringify(i)
                            },
                            _getPlainPKCS8HexFromEncryptedPKCS8PEM: function(t, e) {
                                var r = Ct(t, "ENCRYPTED PRIVATE KEY")
                                  , n = this.parseHexOfEncryptedPKCS8(r)
                                  , i = Ht.getPBKDF2KeyHexFromParam(n, e)
                                  , o = {};
                                o.ciphertext = y.enc.Hex.parse(n.ciphertext);
                                var s = y.enc.Hex.parse(i)
                                  , a = y.enc.Hex.parse(n.encryptionSchemeIV)
                                  , u = y.TripleDES.decrypt(o, s, {
                                    iv: a
                                });
                                return y.enc.Hex.stringify(u)
                            },
                            getKeyFromEncryptedPKCS8PEM: function(t, e) {
                                var r = this._getPlainPKCS8HexFromEncryptedPKCS8PEM(t, e);
                                return this.getKeyFromPlainPrivatePKCS8Hex(r)
                            },
                            parsePlainPrivatePKCS8Hex: function(t) {
                                var e = ft
                                  , r = e.getChildIdx
                                  , n = e.getV
                                  , i = {
                                    algparam: null
                                };
                                if ("30" != t.substr(0, 2))
                                    throw "malformed plain PKCS8 private key(code:001)";
                                var o = r(t, 0);
                                if (3 != o.length)
                                    throw "malformed plain PKCS8 private key(code:002)";
                                if ("30" != t.substr(o[1], 2))
                                    throw "malformed PKCS8 private key(code:003)";
                                var s = r(t, o[1]);
                                if (2 != s.length)
                                    throw "malformed PKCS8 private key(code:004)";
                                if ("06" != t.substr(s[0], 2))
                                    throw "malformed PKCS8 private key(code:005)";
                                if (i.algoid = n(t, s[0]),
                                "06" == t.substr(s[1], 2) && (i.algparam = n(t, s[1])),
                                "04" != t.substr(o[2], 2))
                                    throw "malformed PKCS8 private key(code:006)";
                                return i.keyidx = e.getVidx(t, o[2]),
                                i
                            },
                            getKeyFromPlainPrivatePKCS8PEM: function(t) {
                                var e = Ct(t, "PRIVATE KEY");
                                return this.getKeyFromPlainPrivatePKCS8Hex(e)
                            },
                            getKeyFromPlainPrivatePKCS8Hex: function(t) {
                                var e, r = this.parsePlainPrivatePKCS8Hex(t);
                                if ("2a864886f70d010101" == r.algoid)
                                    e = new it;
                                else if ("2a8648ce380401" == r.algoid)
                                    e = new ct.crypto.DSA;
                                else {
                                    if ("2a8648ce3d0201" != r.algoid)
                                        throw "unsupported private key algorithm";
                                    e = new ct.crypto.ECDSA
                                }
                                return e.readPKCS8PrvKeyHex(t),
                                e
                            },
                            _getKeyFromPublicPKCS8Hex: function(t) {
                                var e, r = ft.getVbyList(t, 0, [0, 0], "06");
                                if ("2a864886f70d010101" === r)
                                    e = new it;
                                else if ("2a8648ce380401" === r)
                                    e = new ct.crypto.DSA;
                                else {
                                    if ("2a8648ce3d0201" !== r)
                                        throw "unsupported PKCS#8 public key hex";
                                    e = new ct.crypto.ECDSA
                                }
                                return e.readPKCS8PubKeyHex(t),
                                e
                            },
                            parsePublicRawRSAKeyHex: function(t) {
                                var e = ft
                                  , r = e.getChildIdx
                                  , n = e.getV
                                  , i = {};
                                if ("30" != t.substr(0, 2))
                                    throw "malformed RSA key(code:001)";
                                var o = r(t, 0);
                                if (2 != o.length)
                                    throw "malformed RSA key(code:002)";
                                if ("02" != t.substr(o[0], 2))
                                    throw "malformed RSA key(code:003)";
                                if (i.n = n(t, o[0]),
                                "02" != t.substr(o[1], 2))
                                    throw "malformed RSA key(code:004)";
                                return i.e = n(t, o[1]),
                                i
                            },
                            parsePublicPKCS8Hex: function(t) {
                                var e = ft
                                  , r = e.getChildIdx
                                  , n = e.getV
                                  , i = {
                                    algparam: null
                                }
                                  , o = r(t, 0);
                                if (2 != o.length)
                                    throw "outer DERSequence shall have 2 elements: " + o.length;
                                var s = o[0];
                                if ("30" != t.substr(s, 2))
                                    throw "malformed PKCS8 public key(code:001)";
                                var a = r(t, s);
                                if (2 != a.length)
                                    throw "malformed PKCS8 public key(code:002)";
                                if ("06" != t.substr(a[0], 2))
                                    throw "malformed PKCS8 public key(code:003)";
                                if (i.algoid = n(t, a[0]),
                                "06" == t.substr(a[1], 2) ? i.algparam = n(t, a[1]) : "30" == t.substr(a[1], 2) && (i.algparam = {},
                                i.algparam.p = e.getVbyList(t, a[1], [0], "02"),
                                i.algparam.q = e.getVbyList(t, a[1], [1], "02"),
                                i.algparam.g = e.getVbyList(t, a[1], [2], "02")),
                                "03" != t.substr(o[1], 2))
                                    throw "malformed PKCS8 public key(code:004)";
                                return i.key = n(t, o[1]).substr(2),
                                i
                            }
                        }
                    }();
                    Ht.getKey = function(t, e, r) {
                        var n, i = (y = ft).getChildIdx, o = (y.getV,
                        y.getVbyList), s = ct.crypto, a = s.ECDSA, u = s.DSA, c = it, h = Ct, l = Ht;
                        if (void 0 !== c && t instanceof c)
                            return t;
                        if (void 0 !== a && t instanceof a)
                            return t;
                        if (void 0 !== u && t instanceof u)
                            return t;
                        if (void 0 !== t.curve && void 0 !== t.xy && void 0 === t.d)
                            return new a({
                                pub: t.xy,
                                curve: t.curve
                            });
                        if (void 0 !== t.curve && void 0 !== t.d)
                            return new a({
                                prv: t.d,
                                curve: t.curve
                            });
                        if (void 0 === t.kty && void 0 !== t.n && void 0 !== t.e && void 0 === t.d)
                            return (C = new c).setPublic(t.n, t.e),
                            C;
                        if (void 0 === t.kty && void 0 !== t.n && void 0 !== t.e && void 0 !== t.d && void 0 !== t.p && void 0 !== t.q && void 0 !== t.dp && void 0 !== t.dq && void 0 !== t.co && void 0 === t.qi)
                            return (C = new c).setPrivateEx(t.n, t.e, t.d, t.p, t.q, t.dp, t.dq, t.co),
                            C;
                        if (void 0 === t.kty && void 0 !== t.n && void 0 !== t.e && void 0 !== t.d && void 0 === t.p)
                            return (C = new c).setPrivate(t.n, t.e, t.d),
                            C;
                        if (void 0 !== t.p && void 0 !== t.q && void 0 !== t.g && void 0 !== t.y && void 0 === t.x)
                            return (C = new u).setPublic(t.p, t.q, t.g, t.y),
                            C;
                        if (void 0 !== t.p && void 0 !== t.q && void 0 !== t.g && void 0 !== t.y && void 0 !== t.x)
                            return (C = new u).setPrivate(t.p, t.q, t.g, t.y, t.x),
                            C;
                        if ("RSA" === t.kty && void 0 !== t.n && void 0 !== t.e && void 0 === t.d)
                            return (C = new c).setPublic(St(t.n), St(t.e)),
                            C;
                        if ("RSA" === t.kty && void 0 !== t.n && void 0 !== t.e && void 0 !== t.d && void 0 !== t.p && void 0 !== t.q && void 0 !== t.dp && void 0 !== t.dq && void 0 !== t.qi)
                            return (C = new c).setPrivateEx(St(t.n), St(t.e), St(t.d), St(t.p), St(t.q), St(t.dp), St(t.dq), St(t.qi)),
                            C;
                        if ("RSA" === t.kty && void 0 !== t.n && void 0 !== t.e && void 0 !== t.d)
                            return (C = new c).setPrivate(St(t.n), St(t.e), St(t.d)),
                            C;
                        if ("EC" === t.kty && void 0 !== t.crv && void 0 !== t.x && void 0 !== t.y && void 0 === t.d) {
                            var f = (P = new a({
                                curve: t.crv
                            })).ecparams.keylen / 4
                              , g = "04" + ("0000000000" + St(t.x)).slice(-f) + ("0000000000" + St(t.y)).slice(-f);
                            return P.setPublicKeyHex(g),
                            P
                        }
                        if ("EC" === t.kty && void 0 !== t.crv && void 0 !== t.x && void 0 !== t.y && void 0 !== t.d) {
                            f = (P = new a({
                                curve: t.crv
                            })).ecparams.keylen / 4,
                            g = "04" + ("0000000000" + St(t.x)).slice(-f) + ("0000000000" + St(t.y)).slice(-f);
                            var d = ("0000000000" + St(t.d)).slice(-f);
                            return P.setPublicKeyHex(g),
                            P.setPrivateKeyHex(d),
                            P
                        }
                        if ("pkcs5prv" === r) {
                            var p, v = t, y = ft;
                            if (9 === (p = i(v, 0)).length)
                                (C = new c).readPKCS5PrvKeyHex(v);
                            else if (6 === p.length)
                                (C = new u).readPKCS5PrvKeyHex(v);
                            else {
                                if (!(p.length > 2 && "04" === v.substr(p[1], 2)))
                                    throw "unsupported PKCS#1/5 hexadecimal key";
                                (C = new a).readPKCS5PrvKeyHex(v)
                            }
                            return C
                        }
                        if ("pkcs8prv" === r)
                            return l.getKeyFromPlainPrivatePKCS8Hex(t);
                        if ("pkcs8pub" === r)
                            return l._getKeyFromPublicPKCS8Hex(t);
                        if ("x509pub" === r)
                            return Wt.getPublicKeyFromCertHex(t);
                        if (-1 != t.indexOf("-END CERTIFICATE-", 0) || -1 != t.indexOf("-END X509 CERTIFICATE-", 0) || -1 != t.indexOf("-END TRUSTED CERTIFICATE-", 0))
                            return Wt.getPublicKeyFromCertPEM(t);
                        if (-1 != t.indexOf("-END PUBLIC KEY-")) {
                            var m = Ct(t, "PUBLIC KEY");
                            return l._getKeyFromPublicPKCS8Hex(m)
                        }
                        if (-1 != t.indexOf("-END RSA PRIVATE KEY-") && -1 == t.indexOf("4,ENCRYPTED")) {
                            var _ = h(t, "RSA PRIVATE KEY");
                            return l.getKey(_, null, "pkcs5prv")
                        }
                        if (-1 != t.indexOf("-END DSA PRIVATE KEY-") && -1 == t.indexOf("4,ENCRYPTED")) {
                            var S = o(n = h(t, "DSA PRIVATE KEY"), 0, [1], "02")
                              , w = o(n, 0, [2], "02")
                              , b = o(n, 0, [3], "02")
                              , E = o(n, 0, [4], "02")
                              , x = o(n, 0, [5], "02");
                            return (C = new u).setPrivate(new F(S,16), new F(w,16), new F(b,16), new F(E,16), new F(x,16)),
                            C
                        }
                        if (-1 != t.indexOf("-END EC PRIVATE KEY-") && -1 == t.indexOf("4,ENCRYPTED"))
                            return _ = h(t, "EC PRIVATE KEY"),
                            l.getKey(_, null, "pkcs5prv");
                        if (-1 != t.indexOf("-END PRIVATE KEY-"))
                            return l.getKeyFromPlainPrivatePKCS8PEM(t);
                        if (-1 != t.indexOf("-END RSA PRIVATE KEY-") && -1 != t.indexOf("4,ENCRYPTED")) {
                            var A = l.getDecryptedKeyHex(t, e)
                              , k = new it;
                            return k.readPKCS5PrvKeyHex(A),
                            k
                        }
                        if (-1 != t.indexOf("-END EC PRIVATE KEY-") && -1 != t.indexOf("4,ENCRYPTED")) {
                            var P, C = o(n = l.getDecryptedKeyHex(t, e), 0, [1], "04"), T = o(n, 0, [2, 0], "06"), R = o(n, 0, [3, 0], "03").substr(2);
                            if (void 0 === ct.crypto.OID.oidhex2name[T])
                                throw "undefined OID(hex) in KJUR.crypto.OID: " + T;
                            return (P = new a({
                                curve: ct.crypto.OID.oidhex2name[T]
                            })).setPublicKeyHex(R),
                            P.setPrivateKeyHex(C),
                            P.isPublic = !1,
                            P
                        }
                        if (-1 != t.indexOf("-END DSA PRIVATE KEY-") && -1 != t.indexOf("4,ENCRYPTED"))
                            return S = o(n = l.getDecryptedKeyHex(t, e), 0, [1], "02"),
                            w = o(n, 0, [2], "02"),
                            b = o(n, 0, [3], "02"),
                            E = o(n, 0, [4], "02"),
                            x = o(n, 0, [5], "02"),
                            (C = new u).setPrivate(new F(S,16), new F(w,16), new F(b,16), new F(E,16), new F(x,16)),
                            C;
                        if (-1 != t.indexOf("-END ENCRYPTED PRIVATE KEY-"))
                            return l.getKeyFromEncryptedPKCS8PEM(t, e);
                        throw new Error("not supported argument")
                    }
                    ,
                    Ht.generateKeypair = function(t, e) {
                        if ("RSA" == t) {
                            var r = e;
                            (s = new it).generate(r, "10001"),
                            s.isPrivate = !0,
                            s.isPublic = !0;
                            var n = new it
                              , i = s.n.toString(16)
                              , o = s.e.toString(16);
                            return n.setPublic(i, o),
                            n.isPrivate = !1,
                            n.isPublic = !0,
                            (a = {}).prvKeyObj = s,
                            a.pubKeyObj = n,
                            a
                        }
                        if ("EC" == t) {
                            var s, a, u = e, c = new ct.crypto.ECDSA({
                                curve: u
                            }).generateKeyPairHex();
                            return (s = new ct.crypto.ECDSA({
                                curve: u
                            })).setPublicKeyHex(c.ecpubhex),
                            s.setPrivateKeyHex(c.ecprvhex),
                            s.isPrivate = !0,
                            s.isPublic = !1,
                            (n = new ct.crypto.ECDSA({
                                curve: u
                            })).setPublicKeyHex(c.ecpubhex),
                            n.isPrivate = !1,
                            n.isPublic = !0,
                            (a = {}).prvKeyObj = s,
                            a.pubKeyObj = n,
                            a
                        }
                        throw "unknown algorithm: " + t
                    }
                    ,
                    Ht.getPEM = function(t, e, r, n, i, o) {
                        var s = ct
                          , a = s.asn1
                          , u = a.DERObjectIdentifier
                          , c = a.DERInteger
                          , h = a.ASN1Util.newObject
                          , l = a.x509.SubjectPublicKeyInfo
                          , f = s.crypto
                          , g = f.DSA
                          , d = f.ECDSA
                          , p = it;
                        function v(t) {
                            return h({
                                seq: [{
                                    int: 0
                                }, {
                                    int: {
                                        bigint: t.n
                                    }
                                }, {
                                    int: t.e
                                }, {
                                    int: {
                                        bigint: t.d
                                    }
                                }, {
                                    int: {
                                        bigint: t.p
                                    }
                                }, {
                                    int: {
                                        bigint: t.q
                                    }
                                }, {
                                    int: {
                                        bigint: t.dmp1
                                    }
                                }, {
                                    int: {
                                        bigint: t.dmq1
                                    }
                                }, {
                                    int: {
                                        bigint: t.coeff
                                    }
                                }]
                            })
                        }
                        function m(t) {
                            return h({
                                seq: [{
                                    int: 1
                                }, {
                                    octstr: {
                                        hex: t.prvKeyHex
                                    }
                                }, {
                                    tag: ["a0", !0, {
                                        oid: {
                                            name: t.curveName
                                        }
                                    }]
                                }, {
                                    tag: ["a1", !0, {
                                        bitstr: {
                                            hex: "00" + t.pubKeyHex
                                        }
                                    }]
                                }]
                            })
                        }
                        function _(t) {
                            return h({
                                seq: [{
                                    int: 0
                                }, {
                                    int: {
                                        bigint: t.p
                                    }
                                }, {
                                    int: {
                                        bigint: t.q
                                    }
                                }, {
                                    int: {
                                        bigint: t.g
                                    }
                                }, {
                                    int: {
                                        bigint: t.y
                                    }
                                }, {
                                    int: {
                                        bigint: t.x
                                    }
                                }]
                            })
                        }
                        if ((void 0 !== p && t instanceof p || void 0 !== g && t instanceof g || void 0 !== d && t instanceof d) && 1 == t.isPublic && (void 0 === e || "PKCS8PUB" == e))
                            return Pt(F = new l(t).getEncodedHex(), "PUBLIC KEY");
                        if ("PKCS1PRV" == e && void 0 !== p && t instanceof p && (void 0 === r || null == r) && 1 == t.isPrivate)
                            return Pt(F = v(t).getEncodedHex(), "RSA PRIVATE KEY");
                        if ("PKCS1PRV" == e && void 0 !== d && t instanceof d && (void 0 === r || null == r) && 1 == t.isPrivate) {
                            var S = new u({
                                name: t.curveName
                            }).getEncodedHex()
                              , w = m(t).getEncodedHex()
                              , b = "";
                            return (b += Pt(S, "EC PARAMETERS")) + Pt(w, "EC PRIVATE KEY")
                        }
                        if ("PKCS1PRV" == e && void 0 !== g && t instanceof g && (void 0 === r || null == r) && 1 == t.isPrivate)
                            return Pt(F = _(t).getEncodedHex(), "DSA PRIVATE KEY");
                        if ("PKCS5PRV" == e && void 0 !== p && t instanceof p && void 0 !== r && null != r && 1 == t.isPrivate) {
                            var F = v(t).getEncodedHex();
                            return void 0 === n && (n = "DES-EDE3-CBC"),
                            this.getEncryptedPKCS5PEMFromPrvKeyHex("RSA", F, r, n, o)
                        }
                        if ("PKCS5PRV" == e && void 0 !== d && t instanceof d && void 0 !== r && null != r && 1 == t.isPrivate)
                            return F = m(t).getEncodedHex(),
                            void 0 === n && (n = "DES-EDE3-CBC"),
                            this.getEncryptedPKCS5PEMFromPrvKeyHex("EC", F, r, n, o);
                        if ("PKCS5PRV" == e && void 0 !== g && t instanceof g && void 0 !== r && null != r && 1 == t.isPrivate)
                            return F = _(t).getEncodedHex(),
                            void 0 === n && (n = "DES-EDE3-CBC"),
                            this.getEncryptedPKCS5PEMFromPrvKeyHex("DSA", F, r, n, o);
                        var E = function(t, e) {
                            var r = x(t, e);
                            return new h({
                                seq: [{
                                    seq: [{
                                        oid: {
                                            name: "pkcs5PBES2"
                                        }
                                    }, {
                                        seq: [{
                                            seq: [{
                                                oid: {
                                                    name: "pkcs5PBKDF2"
                                                }
                                            }, {
                                                seq: [{
                                                    octstr: {
                                                        hex: r.pbkdf2Salt
                                                    }
                                                }, {
                                                    int: r.pbkdf2Iter
                                                }]
                                            }]
                                        }, {
                                            seq: [{
                                                oid: {
                                                    name: "des-EDE3-CBC"
                                                }
                                            }, {
                                                octstr: {
                                                    hex: r.encryptionSchemeIV
                                                }
                                            }]
                                        }]
                                    }]
                                }, {
                                    octstr: {
                                        hex: r.ciphertext
                                    }
                                }]
                            }).getEncodedHex()
                        }
                          , x = function(t, e) {
                            var r = y.lib.WordArray.random(8)
                              , n = y.lib.WordArray.random(8)
                              , i = y.PBKDF2(e, r, {
                                keySize: 6,
                                iterations: 100
                            })
                              , o = y.enc.Hex.parse(t)
                              , s = y.TripleDES.encrypt(o, i, {
                                iv: n
                            }) + ""
                              , a = {};
                            return a.ciphertext = s,
                            a.pbkdf2Salt = y.enc.Hex.stringify(r),
                            a.pbkdf2Iter = 100,
                            a.encryptionSchemeAlg = "DES-EDE3-CBC",
                            a.encryptionSchemeIV = y.enc.Hex.stringify(n),
                            a
                        };
                        if ("PKCS8PRV" == e && null != p && t instanceof p && 1 == t.isPrivate) {
                            var A = v(t).getEncodedHex();
                            return F = h({
                                seq: [{
                                    int: 0
                                }, {
                                    seq: [{
                                        oid: {
                                            name: "rsaEncryption"
                                        }
                                    }, {
                                        null: !0
                                    }]
                                }, {
                                    octstr: {
                                        hex: A
                                    }
                                }]
                            }).getEncodedHex(),
                            void 0 === r || null == r ? Pt(F, "PRIVATE KEY") : Pt(w = E(F, r), "ENCRYPTED PRIVATE KEY")
                        }
                        if ("PKCS8PRV" == e && void 0 !== d && t instanceof d && 1 == t.isPrivate)
                            return A = new h({
                                seq: [{
                                    int: 1
                                }, {
                                    octstr: {
                                        hex: t.prvKeyHex
                                    }
                                }, {
                                    tag: ["a1", !0, {
                                        bitstr: {
                                            hex: "00" + t.pubKeyHex
                                        }
                                    }]
                                }]
                            }).getEncodedHex(),
                            F = h({
                                seq: [{
                                    int: 0
                                }, {
                                    seq: [{
                                        oid: {
                                            name: "ecPublicKey"
                                        }
                                    }, {
                                        oid: {
                                            name: t.curveName
                                        }
                                    }]
                                }, {
                                    octstr: {
                                        hex: A
                                    }
                                }]
                            }).getEncodedHex(),
                            void 0 === r || null == r ? Pt(F, "PRIVATE KEY") : Pt(w = E(F, r), "ENCRYPTED PRIVATE KEY");
                        if ("PKCS8PRV" == e && void 0 !== g && t instanceof g && 1 == t.isPrivate)
                            return A = new c({
                                bigint: t.x
                            }).getEncodedHex(),
                            F = h({
                                seq: [{
                                    int: 0
                                }, {
                                    seq: [{
                                        oid: {
                                            name: "dsa"
                                        }
                                    }, {
                                        seq: [{
                                            int: {
                                                bigint: t.p
                                            }
                                        }, {
                                            int: {
                                                bigint: t.q
                                            }
                                        }, {
                                            int: {
                                                bigint: t.g
                                            }
                                        }]
                                    }]
                                }, {
                                    octstr: {
                                        hex: A
                                    }
                                }]
                            }).getEncodedHex(),
                            void 0 === r || null == r ? Pt(F, "PRIVATE KEY") : Pt(w = E(F, r), "ENCRYPTED PRIVATE KEY");
                        throw new Error("unsupported object nor format")
                    }
                    ,
                    Ht.getKeyFromCSRPEM = function(t) {
                        var e = Ct(t, "CERTIFICATE REQUEST");
                        return Ht.getKeyFromCSRHex(e)
                    }
                    ,
                    Ht.getKeyFromCSRHex = function(t) {
                        var e = Ht.parseCSRHex(t);
                        return Ht.getKey(e.p8pubkeyhex, null, "pkcs8pub")
                    }
                    ,
                    Ht.parseCSRHex = function(t) {
                        var e = ft
                          , r = e.getChildIdx
                          , n = e.getTLV
                          , i = {}
                          , o = t;
                        if ("30" != o.substr(0, 2))
                            throw "malformed CSR(code:001)";
                        var s = r(o, 0);
                        if (s.length < 1)
                            throw "malformed CSR(code:002)";
                        if ("30" != o.substr(s[0], 2))
                            throw "malformed CSR(code:003)";
                        var a = r(o, s[0]);
                        if (a.length < 3)
                            throw "malformed CSR(code:004)";
                        return i.p8pubkeyhex = n(o, a[2]),
                        i
                    }
                    ,
                    Ht.getKeyID = function(t) {
                        var e = Ht
                          , r = ft;
                        "string" == typeof t && -1 != t.indexOf("BEGIN ") && (t = e.getKey(t));
                        var n = Ct(e.getPEM(t))
                          , i = r.getIdxbyList(n, 0, [1])
                          , o = r.getV(n, i).substring(2);
                        return ct.crypto.Util.hashHex(o, "sha1")
                    }
                    ,
                    Ht.getJWKFromKey = function(t) {
                        var e = {};
                        if (t instanceof it && t.isPrivate)
                            return e.kty = "RSA",
                            e.n = _t(t.n.toString(16)),
                            e.e = _t(t.e.toString(16)),
                            e.d = _t(t.d.toString(16)),
                            e.p = _t(t.p.toString(16)),
                            e.q = _t(t.q.toString(16)),
                            e.dp = _t(t.dmp1.toString(16)),
                            e.dq = _t(t.dmq1.toString(16)),
                            e.qi = _t(t.coeff.toString(16)),
                            e;
                        if (t instanceof it && t.isPublic)
                            return e.kty = "RSA",
                            e.n = _t(t.n.toString(16)),
                            e.e = _t(t.e.toString(16)),
                            e;
                        if (t instanceof ct.crypto.ECDSA && t.isPrivate) {
                            if ("P-256" !== (n = t.getShortNISTPCurveName()) && "P-384" !== n)
                                throw "unsupported curve name for JWT: " + n;
                            var r = t.getPublicKeyXYHex();
                            return e.kty = "EC",
                            e.crv = n,
                            e.x = _t(r.x),
                            e.y = _t(r.y),
                            e.d = _t(t.prvKeyHex),
                            e
                        }
                        if (t instanceof ct.crypto.ECDSA && t.isPublic) {
                            var n;
                            if ("P-256" !== (n = t.getShortNISTPCurveName()) && "P-384" !== n)
                                throw "unsupported curve name for JWT: " + n;
                            return r = t.getPublicKeyXYHex(),
                            e.kty = "EC",
                            e.crv = n,
                            e.x = _t(r.x),
                            e.y = _t(r.y),
                            e
                        }
                        throw "not supported key object"
                    }
                    ,
                    it.getPosArrayOfChildrenFromHex = function(t) {
                        return ft.getChildIdx(t, 0)
                    }
                    ,
                    it.getHexValueArrayOfChildrenFromHex = function(t) {
                        var e, r = ft.getV, n = r(t, (e = it.getPosArrayOfChildrenFromHex(t))[0]), i = r(t, e[1]), o = r(t, e[2]), s = r(t, e[3]), a = r(t, e[4]), u = r(t, e[5]), c = r(t, e[6]), h = r(t, e[7]), l = r(t, e[8]);
                        return (e = new Array).push(n, i, o, s, a, u, c, h, l),
                        e
                    }
                    ,
                    it.prototype.readPrivateKeyFromPEMString = function(t) {
                        var e = Ct(t)
                          , r = it.getHexValueArrayOfChildrenFromHex(e);
                        this.setPrivateEx(r[1], r[2], r[3], r[4], r[5], r[6], r[7], r[8])
                    }
                    ,
                    it.prototype.readPKCS5PrvKeyHex = function(t) {
                        var e = it.getHexValueArrayOfChildrenFromHex(t);
                        this.setPrivateEx(e[1], e[2], e[3], e[4], e[5], e[6], e[7], e[8])
                    }
                    ,
                    it.prototype.readPKCS8PrvKeyHex = function(t) {
                        var e, r, n, i, o, s, a, u, c = ft, h = c.getVbyListEx;
                        if (!1 === c.isASN1HEX(t))
                            throw new Error("not ASN.1 hex string");
                        try {
                            e = h(t, 0, [2, 0, 1], "02"),
                            r = h(t, 0, [2, 0, 2], "02"),
                            n = h(t, 0, [2, 0, 3], "02"),
                            i = h(t, 0, [2, 0, 4], "02"),
                            o = h(t, 0, [2, 0, 5], "02"),
                            s = h(t, 0, [2, 0, 6], "02"),
                            a = h(t, 0, [2, 0, 7], "02"),
                            u = h(t, 0, [2, 0, 8], "02")
                        } catch (t) {
                            throw new Error("malformed PKCS#8 plain RSA private key")
                        }
                        this.setPrivateEx(e, r, n, i, o, s, a, u)
                    }
                    ,
                    it.prototype.readPKCS5PubKeyHex = function(t) {
                        var e = ft
                          , r = e.getV;
                        if (!1 === e.isASN1HEX(t))
                            throw new Error("keyHex is not ASN.1 hex string");
                        var n = e.getChildIdx(t, 0);
                        if (2 !== n.length || "02" !== t.substr(n[0], 2) || "02" !== t.substr(n[1], 2))
                            throw new Error("wrong hex for PKCS#5 public key");
                        var i = r(t, n[0])
                          , o = r(t, n[1]);
                        this.setPublic(i, o)
                    }
                    ,
                    it.prototype.readPKCS8PubKeyHex = function(t) {
                        var e = ft;
                        if (!1 === e.isASN1HEX(t))
                            throw new Error("not ASN.1 hex string");
                        if ("06092a864886f70d010101" !== e.getTLVbyListEx(t, 0, [0, 0]))
                            throw new Error("not PKCS8 RSA public key");
                        var r = e.getTLVbyListEx(t, 0, [1, 0]);
                        this.readPKCS5PubKeyHex(r)
                    }
                    ,
                    it.prototype.readCertPubKeyHex = function(t, e) {
                        var r, n;
                        (r = new Wt).readCertHex(t),
                        n = r.getPublicKeyHex(),
                        this.readPKCS8PubKeyHex(n)
                    }
                    ;
                    var Kt = new RegExp("[^0-9a-f]","gi");
                    function Vt(t, e) {
                        for (var r = "", n = e / 4 - t.length, i = 0; i < n; i++)
                            r += "0";
                        return r + t
                    }
                    function qt(t, e, r) {
                        for (var n = "", i = 0; n.length < e; )
                            n += Ft(r(Et(t + String.fromCharCode.apply(String, [(4278190080 & i) >> 24, (16711680 & i) >> 16, (65280 & i) >> 8, 255 & i])))),
                            i += 1;
                        return n
                    }
                    function Jt(t) {
                        for (var e in ct.crypto.Util.DIGESTINFOHEAD) {
                            var r = ct.crypto.Util.DIGESTINFOHEAD[e]
                              , n = r.length;
                            if (t.substring(0, n) == r)
                                return [e, t.substring(n)]
                        }
                        return []
                    }
                    function Wt(t) {
                        var e, r = ft, n = r.getChildIdx, i = r.getV, o = r.getTLV, s = r.getVbyList, a = r.getVbyListEx, u = r.getTLVbyList, c = r.getTLVbyListEx, h = r.getIdxbyList, l = r.getIdxbyListEx, f = r.getVidx, g = r.oidname, d = r.hextooidstr, p = Wt, v = Ct;
                        try {
                            e = ct.asn1.x509.AlgorithmIdentifier.PSSNAME2ASN1TLV
                        } catch (t) {}
                        this.HEX2STAG = {
                            "0c": "utf8",
                            13: "prn",
                            16: "ia5",
                            "1a": "vis",
                            "1e": "bmp"
                        },
                        this.hex = null,
                        this.version = 0,
                        this.foffset = 0,
                        this.aExtInfo = null,
                        this.getVersion = function() {
                            return null === this.hex || 0 !== this.version ? this.version : "a003020102" !== u(this.hex, 0, [0, 0]) ? (this.version = 1,
                            this.foffset = -1,
                            1) : (this.version = 3,
                            3)
                        }
                        ,
                        this.getSerialNumberHex = function() {
                            return a(this.hex, 0, [0, 0], "02")
                        }
                        ,
                        this.getSignatureAlgorithmField = function() {
                            var t = c(this.hex, 0, [0, 1]);
                            return this.getAlgorithmIdentifierName(t)
                        }
                        ,
                        this.getAlgorithmIdentifierName = function(t) {
                            for (var r in e)
                                if (t === e[r])
                                    return r;
                            return g(a(t, 0, [0], "06"))
                        }
                        ,
                        this.getIssuer = function() {
                            return this.getX500Name(this.getIssuerHex())
                        }
                        ,
                        this.getIssuerHex = function() {
                            return u(this.hex, 0, [0, 3 + this.foffset], "30")
                        }
                        ,
                        this.getIssuerString = function() {
                            return p.hex2dn(this.getIssuerHex())
                        }
                        ,
                        this.getSubject = function() {
                            return this.getX500Name(this.getSubjectHex())
                        }
                        ,
                        this.getSubjectHex = function() {
                            return u(this.hex, 0, [0, 5 + this.foffset], "30")
                        }
                        ,
                        this.getSubjectString = function() {
                            return p.hex2dn(this.getSubjectHex())
                        }
                        ,
                        this.getNotBefore = function() {
                            var t = s(this.hex, 0, [0, 4 + this.foffset, 0]);
                            return t = t.replace(/(..)/g, "%$1"),
                            decodeURIComponent(t)
                        }
                        ,
                        this.getNotAfter = function() {
                            var t = s(this.hex, 0, [0, 4 + this.foffset, 1]);
                            return t = t.replace(/(..)/g, "%$1"),
                            decodeURIComponent(t)
                        }
                        ,
                        this.getPublicKeyHex = function() {
                            return r.getTLVbyList(this.hex, 0, [0, 6 + this.foffset], "30")
                        }
                        ,
                        this.getPublicKeyIdx = function() {
                            return h(this.hex, 0, [0, 6 + this.foffset], "30")
                        }
                        ,
                        this.getPublicKeyContentIdx = function() {
                            var t = this.getPublicKeyIdx();
                            return h(this.hex, t, [1, 0], "30")
                        }
                        ,
                        this.getPublicKey = function() {
                            return Ht.getKey(this.getPublicKeyHex(), null, "pkcs8pub")
                        }
                        ,
                        this.getSignatureAlgorithmName = function() {
                            var t = u(this.hex, 0, [1], "30");
                            return this.getAlgorithmIdentifierName(t)
                        }
                        ,
                        this.getSignatureValueHex = function() {
                            return s(this.hex, 0, [2], "03", !0)
                        }
                        ,
                        this.verifySignature = function(t) {
                            var e = this.getSignatureAlgorithmField()
                              , r = this.getSignatureValueHex()
                              , n = u(this.hex, 0, [0], "30")
                              , i = new ct.crypto.Signature({
                                alg: e
                            });
                            return i.init(t),
                            i.updateHex(n),
                            i.verify(r)
                        }
                        ,
                        this.parseExt = function(t) {
                            var e, o, a;
                            if (void 0 === t) {
                                if (a = this.hex,
                                3 !== this.version)
                                    return -1;
                                e = h(a, 0, [0, 7, 0], "30"),
                                o = n(a, e)
                            } else {
                                a = Ct(t);
                                var u = h(a, 0, [0, 3, 0, 0], "06");
                                if ("2a864886f70d01090e" != i(a, u))
                                    return void (this.aExtInfo = new Array);
                                e = h(a, 0, [0, 3, 0, 1, 0], "30"),
                                o = n(a, e),
                                this.hex = a
                            }
                            this.aExtInfo = new Array;
                            for (var c = 0; c < o.length; c++) {
                                var l = {
                                    critical: !1
                                }
                                  , g = 0;
                                3 === n(a, o[c]).length && (l.critical = !0,
                                g = 1),
                                l.oid = r.hextooidstr(s(a, o[c], [0], "06"));
                                var d = h(a, o[c], [1 + g]);
                                l.vidx = f(a, d),
                                this.aExtInfo.push(l)
                            }
                        }
                        ,
                        this.getExtInfo = function(t) {
                            var e = this.aExtInfo
                              , r = t;
                            if (t.match(/^[0-9.]+$/) || (r = ct.asn1.x509.OID.name2oid(t)),
                            "" !== r)
                                for (var n = 0; n < e.length; n++)
                                    if (e[n].oid === r)
                                        return e[n]
                        }
                        ,
                        this.getExtBasicConstraints = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("basicConstraints");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var n = {
                                extname: "basicConstraints"
                            };
                            if (e && (n.critical = !0),
                            "3000" === t)
                                return n;
                            if ("30030101ff" === t)
                                return n.cA = !0,
                                n;
                            if ("30060101ff02" === t.substr(0, 12)) {
                                var s = i(t, 10)
                                  , a = parseInt(s, 16);
                                return n.cA = !0,
                                n.pathLen = a,
                                n
                            }
                            throw new Error("hExtV parse error: " + t)
                        }
                        ,
                        this.getExtKeyUsage = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("keyUsage");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var n = {
                                extname: "keyUsage"
                            };
                            return e && (n.critical = !0),
                            n.names = this.getExtKeyUsageString(t).split(","),
                            n
                        }
                        ,
                        this.getExtKeyUsageBin = function(t) {
                            if (void 0 === t) {
                                var e = this.getExtInfo("keyUsage");
                                if (void 0 === e)
                                    return "";
                                t = o(this.hex, e.vidx)
                            }
                            if (8 != t.length && 10 != t.length)
                                throw new Error("malformed key usage value: " + t);
                            var r = "000000000000000" + parseInt(t.substr(6), 16).toString(2);
                            return 8 == t.length && (r = r.slice(-8)),
                            10 == t.length && (r = r.slice(-16)),
                            "" == (r = r.replace(/0+$/, "")) && (r = "0"),
                            r
                        }
                        ,
                        this.getExtKeyUsageString = function(t) {
                            for (var e = this.getExtKeyUsageBin(t), r = new Array, n = 0; n < e.length; n++)
                                "1" == e.substr(n, 1) && r.push(Wt.KEYUSAGE_NAME[n]);
                            return r.join(",")
                        }
                        ,
                        this.getExtSubjectKeyIdentifier = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("subjectKeyIdentifier");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var n = {
                                extname: "subjectKeyIdentifier"
                            };
                            e && (n.critical = !0);
                            var s = i(t, 0);
                            return n.kid = {
                                hex: s
                            },
                            n
                        }
                        ,
                        this.getExtAuthorityKeyIdentifier = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("authorityKeyIdentifier");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var s = {
                                extname: "authorityKeyIdentifier"
                            };
                            e && (s.critical = !0);
                            for (var a = n(t, 0), u = 0; u < a.length; u++) {
                                var c = t.substr(a[u], 2);
                                if ("80" === c && (s.kid = {
                                    hex: i(t, a[u])
                                }),
                                "a1" === c) {
                                    var h = o(t, a[u])
                                      , l = this.getGeneralNames(h);
                                    s.issuer = l[0].dn
                                }
                                "82" === c && (s.sn = {
                                    hex: i(t, a[u])
                                })
                            }
                            return s
                        }
                        ,
                        this.getExtExtKeyUsage = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("extKeyUsage");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var s = {
                                extname: "extKeyUsage",
                                array: []
                            };
                            e && (s.critical = !0);
                            for (var a = n(t, 0), u = 0; u < a.length; u++)
                                s.array.push(g(i(t, a[u])));
                            return s
                        }
                        ,
                        this.getExtExtKeyUsageName = function() {
                            var t = this.getExtInfo("extKeyUsage");
                            if (void 0 === t)
                                return t;
                            var e = new Array
                              , r = o(this.hex, t.vidx);
                            if ("" === r)
                                return e;
                            for (var s = n(r, 0), a = 0; a < s.length; a++)
                                e.push(g(i(r, s[a])));
                            return e
                        }
                        ,
                        this.getExtSubjectAltName = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("subjectAltName");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var n = {
                                extname: "subjectAltName",
                                array: []
                            };
                            return e && (n.critical = !0),
                            n.array = this.getGeneralNames(t),
                            n
                        }
                        ,
                        this.getExtIssuerAltName = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("issuerAltName");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var n = {
                                extname: "issuerAltName",
                                array: []
                            };
                            return e && (n.critical = !0),
                            n.array = this.getGeneralNames(t),
                            n
                        }
                        ,
                        this.getGeneralNames = function(t) {
                            for (var e = n(t, 0), r = [], i = 0; i < e.length; i++) {
                                var s = this.getGeneralName(o(t, e[i]));
                                void 0 !== s && r.push(s)
                            }
                            return r
                        }
                        ,
                        this.getGeneralName = function(t) {
                            var e = t.substr(0, 2)
                              , r = i(t, 0)
                              , n = Ft(r);
                            return "81" == e ? {
                                rfc822: n
                            } : "82" == e ? {
                                dns: n
                            } : "86" == e ? {
                                uri: n
                            } : "87" == e ? {
                                ip: Ut(r)
                            } : "a4" == e ? {
                                dn: this.getX500Name(r)
                            } : void 0
                        }
                        ,
                        this.getExtSubjectAltName2 = function() {
                            var t, e, r, s = this.getExtInfo("subjectAltName");
                            if (void 0 === s)
                                return s;
                            for (var a = new Array, u = o(this.hex, s.vidx), c = n(u, 0), h = 0; h < c.length; h++)
                                r = u.substr(c[h], 2),
                                t = i(u, c[h]),
                                "81" === r && (e = bt(t),
                                a.push(["MAIL", e])),
                                "82" === r && (e = bt(t),
                                a.push(["DNS", e])),
                                "84" === r && (e = Wt.hex2dn(t, 0),
                                a.push(["DN", e])),
                                "86" === r && (e = bt(t),
                                a.push(["URI", e])),
                                "87" === r && (e = Ut(t),
                                a.push(["IP", e]));
                            return a
                        }
                        ,
                        this.getExtCRLDistributionPoints = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("cRLDistributionPoints");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var i = {
                                extname: "cRLDistributionPoints",
                                array: []
                            };
                            e && (i.critical = !0);
                            for (var s = n(t, 0), a = 0; a < s.length; a++) {
                                var u = o(t, s[a]);
                                i.array.push(this.getDistributionPoint(u))
                            }
                            return i
                        }
                        ,
                        this.getDistributionPoint = function(t) {
                            for (var e = {}, r = n(t, 0), i = 0; i < r.length; i++) {
                                var s = t.substr(r[i], 2)
                                  , a = o(t, r[i]);
                                "a0" == s && (e.dpname = this.getDistributionPointName(a))
                            }
                            return e
                        }
                        ,
                        this.getDistributionPointName = function(t) {
                            for (var e = {}, r = n(t, 0), i = 0; i < r.length; i++) {
                                var s = t.substr(r[i], 2)
                                  , a = o(t, r[i]);
                                "a0" == s && (e.full = this.getGeneralNames(a))
                            }
                            return e
                        }
                        ,
                        this.getExtCRLDistributionPointsURI = function() {
                            var t = this.getExtInfo("cRLDistributionPoints");
                            if (void 0 === t)
                                return t;
                            for (var e = new Array, r = n(this.hex, t.vidx), i = 0; i < r.length; i++)
                                try {
                                    var o = bt(s(this.hex, r[i], [0, 0, 0], "86"));
                                    e.push(o)
                                } catch (t) {}
                            return e
                        }
                        ,
                        this.getExtAIAInfo = function() {
                            var t = this.getExtInfo("authorityInfoAccess");
                            if (void 0 === t)
                                return t;
                            for (var e = {
                                ocsp: [],
                                caissuer: []
                            }, r = n(this.hex, t.vidx), i = 0; i < r.length; i++) {
                                var o = s(this.hex, r[i], [0], "06")
                                  , a = s(this.hex, r[i], [1], "86");
                                "2b06010505073001" === o && e.ocsp.push(bt(a)),
                                "2b06010505073002" === o && e.caissuer.push(bt(a))
                            }
                            return e
                        }
                        ,
                        this.getExtAuthorityInfoAccess = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("authorityInfoAccess");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var i = {
                                extname: "authorityInfoAccess",
                                array: []
                            };
                            e && (i.critical = !0);
                            for (var u = n(t, 0), c = 0; c < u.length; c++) {
                                var h = a(t, u[c], [0], "06")
                                  , l = bt(s(t, u[c], [1], "86"));
                                if ("2b06010505073001" == h)
                                    i.array.push({
                                        ocsp: l
                                    });
                                else {
                                    if ("2b06010505073002" != h)
                                        throw new Error("unknown method: " + h);
                                    i.array.push({
                                        caissuer: l
                                    })
                                }
                            }
                            return i
                        }
                        ,
                        this.getExtCertificatePolicies = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("certificatePolicies");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var i = {
                                extname: "certificatePolicies",
                                array: []
                            };
                            e && (i.critical = !0);
                            for (var s = n(t, 0), a = 0; a < s.length; a++) {
                                var u = o(t, s[a])
                                  , c = this.getPolicyInformation(u);
                                i.array.push(c)
                            }
                            return i
                        }
                        ,
                        this.getPolicyInformation = function(t) {
                            var e = {}
                              , r = s(t, 0, [0], "06");
                            e.policyoid = g(r);
                            var i = l(t, 0, [1], "30");
                            if (-1 != i) {
                                e.array = [];
                                for (var a = n(t, i), u = 0; u < a.length; u++) {
                                    var c = o(t, a[u])
                                      , h = this.getPolicyQualifierInfo(c);
                                    e.array.push(h)
                                }
                            }
                            return e
                        }
                        ,
                        this.getPolicyQualifierInfo = function(t) {
                            var e = {}
                              , r = s(t, 0, [0], "06");
                            if ("2b06010505070201" === r) {
                                var n = a(t, 0, [1], "16");
                                e.cps = Ft(n)
                            } else if ("2b06010505070202" === r) {
                                var i = u(t, 0, [1], "30");
                                e.unotice = this.getUserNotice(i)
                            }
                            return e
                        }
                        ,
                        this.getUserNotice = function(t) {
                            for (var e = {}, r = n(t, 0), i = 0; i < r.length; i++) {
                                var s = o(t, r[i]);
                                "30" != s.substr(0, 2) && (e.exptext = this.getDisplayText(s))
                            }
                            return e
                        }
                        ,
                        this.getDisplayText = function(t) {
                            var e = {};
                            return e.type = {
                                "0c": "utf8",
                                16: "ia5",
                                "1a": "vis",
                                "1e": "bmp"
                            }[t.substr(0, 2)],
                            e.str = Ft(i(t, 0)),
                            e
                        }
                        ,
                        this.getExtCRLNumber = function(t, e) {
                            var r = {
                                extname: "cRLNumber"
                            };
                            if (e && (r.critical = !0),
                            "02" == t.substr(0, 2))
                                return r.num = {
                                    hex: i(t, 0)
                                },
                                r;
                            throw new Error("hExtV parse error: " + t)
                        }
                        ,
                        this.getExtCRLReason = function(t, e) {
                            var r = {
                                extname: "cRLReason"
                            };
                            if (e && (r.critical = !0),
                            "0a" == t.substr(0, 2))
                                return r.code = parseInt(i(t, 0), 16),
                                r;
                            throw new Error("hExtV parse error: " + t)
                        }
                        ,
                        this.getExtOcspNonce = function(t, e) {
                            var r = {
                                extname: "ocspNonce"
                            };
                            e && (r.critical = !0);
                            var n = i(t, 0);
                            return r.hex = n,
                            r
                        }
                        ,
                        this.getExtOcspNoCheck = function(t, e) {
                            var r = {
                                extname: "ocspNoCheck"
                            };
                            return e && (r.critical = !0),
                            r
                        }
                        ,
                        this.getExtAdobeTimeStamp = function(t, e) {
                            if (void 0 === t && void 0 === e) {
                                var r = this.getExtInfo("adobeTimeStamp");
                                if (void 0 === r)
                                    return;
                                t = o(this.hex, r.vidx),
                                e = r.critical
                            }
                            var i = {
                                extname: "adobeTimeStamp"
                            };
                            e && (i.critical = !0);
                            var s = n(t, 0);
                            if (s.length > 1) {
                                var a = o(t, s[1])
                                  , u = this.getGeneralName(a);
                                null != u.uri && (i.uri = u.uri)
                            }
                            if (s.length > 2) {
                                var c = o(t, s[2]);
                                "0101ff" == c && (i.reqauth = !0),
                                "010100" == c && (i.reqauth = !1)
                            }
                            return i
                        }
                        ,
                        this.getX500NameRule = function(t) {
                            for (var e = null, r = [], n = 0; n < t.length; n++)
                                for (var i = t[n], o = 0; o < i.length; o++)
                                    r.push(i[o]);
                            for (n = 0; n < r.length; n++) {
                                var s = r[n]
                                  , a = s.ds
                                  , u = s.value
                                  , c = s.type;
                                if ("prn" != a && "utf8" != a && "ia5" != a)
                                    return "mixed";
                                if ("ia5" == a) {
                                    if ("CN" != c)
                                        return "mixed";
                                    if (ct.lang.String.isMail(u))
                                        continue;
                                    return "mixed"
                                }
                                if ("C" == c) {
                                    if ("prn" == a)
                                        continue;
                                    return "mixed"
                                }
                                if (null == e)
                                    e = a;
                                else if (e !== a)
                                    return "mixed"
                            }
                            return null == e ? "prn" : e
                        }
                        ,
                        this.getX500Name = function(t) {
                            var e = this.getX500NameArray(t);
                            return {
                                array: e,
                                str: this.dnarraytostr(e)
                            }
                        }
                        ,
                        this.getX500NameArray = function(t) {
                            for (var e = [], r = n(t, 0), i = 0; i < r.length; i++)
                                e.push(this.getRDN(o(t, r[i])));
                            return e
                        }
                        ,
                        this.getRDN = function(t) {
                            for (var e = [], r = n(t, 0), i = 0; i < r.length; i++)
                                e.push(this.getAttrTypeAndValue(o(t, r[i])));
                            return e
                        }
                        ,
                        this.getAttrTypeAndValue = function(t) {
                            var e = {
                                type: null,
                                value: null,
                                ds: null
                            }
                              , r = n(t, 0)
                              , i = s(t, r[0], [], "06")
                              , o = s(t, r[1], [])
                              , a = ct.asn1.ASN1Util.oidHexToInt(i);
                            return e.type = ct.asn1.x509.OID.oid2atype(a),
                            e.value = Ft(o),
                            e.ds = this.HEX2STAG[t.substr(r[1], 2)],
                            e
                        }
                        ,
                        this.readCertPEM = function(t) {
                            this.readCertHex(v(t))
                        }
                        ,
                        this.readCertHex = function(t) {
                            this.hex = t,
                            this.getVersion();
                            try {
                                h(this.hex, 0, [0, 7], "a3"),
                                this.parseExt()
                            } catch (t) {}
                        }
                        ,
                        this.getParam = function() {
                            var t = {};
                            return t.version = this.getVersion(),
                            t.serial = {
                                hex: this.getSerialNumberHex()
                            },
                            t.sigalg = this.getSignatureAlgorithmField(),
                            t.issuer = this.getIssuer(),
                            t.notbefore = this.getNotBefore(),
                            t.notafter = this.getNotAfter(),
                            t.subject = this.getSubject(),
                            t.sbjpubkey = Pt(this.getPublicKeyHex(), "PUBLIC KEY"),
                            this.aExtInfo.length > 0 && (t.ext = this.getExtParamArray()),
                            t.sighex = this.getSignatureValueHex(),
                            t
                        }
                        ,
                        this.getExtParamArray = function(t) {
                            null == t && -1 != l(this.hex, 0, [0, "[3]"]) && (t = c(this.hex, 0, [0, "[3]", 0], "30"));
                            for (var e = [], r = n(t, 0), i = 0; i < r.length; i++) {
                                var s = o(t, r[i])
                                  , a = this.getExtParam(s);
                                null != a && e.push(a)
                            }
                            return e
                        }
                        ,
                        this.getExtParam = function(t) {
                            var e = n(t, 0).length;
                            if (2 != e && 3 != e)
                                throw new Error("wrong number elements in Extension: " + e + " " + t);
                            var r = d(s(t, 0, [0], "06"))
                              , i = !1;
                            3 == e && "0101ff" == u(t, 0, [1]) && (i = !0);
                            var o = u(t, 0, [e - 1, 0])
                              , a = void 0;
                            if ("2.5.29.14" == r ? a = this.getExtSubjectKeyIdentifier(o, i) : "2.5.29.15" == r ? a = this.getExtKeyUsage(o, i) : "2.5.29.17" == r ? a = this.getExtSubjectAltName(o, i) : "2.5.29.18" == r ? a = this.getExtIssuerAltName(o, i) : "2.5.29.19" == r ? a = this.getExtBasicConstraints(o, i) : "2.5.29.31" == r ? a = this.getExtCRLDistributionPoints(o, i) : "2.5.29.32" == r ? a = this.getExtCertificatePolicies(o, i) : "2.5.29.35" == r ? a = this.getExtAuthorityKeyIdentifier(o, i) : "2.5.29.37" == r ? a = this.getExtExtKeyUsage(o, i) : "1.3.6.1.5.5.7.1.1" == r ? a = this.getExtAuthorityInfoAccess(o, i) : "2.5.29.20" == r ? a = this.getExtCRLNumber(o, i) : "2.5.29.21" == r ? a = this.getExtCRLReason(o, i) : "1.3.6.1.5.5.7.48.1.2" == r ? a = this.getExtOcspNonce(o, i) : "1.3.6.1.5.5.7.48.1.5" == r ? a = this.getExtOcspNoCheck(o, i) : "1.2.840.113583.1.1.9.1" == r && (a = this.getExtAdobeTimeStamp(o, i)),
                            null != a)
                                return a;
                            var c = {
                                extname: r,
                                extn: o
                            };
                            return i && (c.critical = !0),
                            c
                        }
                        ,
                        this.findExt = function(t, e) {
                            for (var r = 0; r < t.length; r++)
                                if (t[r].extname == e)
                                    return t[r];
                            return null
                        }
                        ,
                        this.updateExtCDPFullURI = function(t, e) {
                            var r = this.findExt(t, "cRLDistributionPoints");
                            if (null != r && null != r.array)
                                for (var n = r.array, i = 0; i < n.length; i++)
                                    if (null != n[i].dpname && null != n[i].dpname.full)
                                        for (var o = n[i].dpname.full, s = 0; s < o.length; s++) {
                                            var a = o[i];
                                            null != a.uri && (a.uri = e)
                                        }
                        }
                        ,
                        this.updateExtAIAOCSP = function(t, e) {
                            var r = this.findExt(t, "authorityInfoAccess");
                            if (null != r && null != r.array)
                                for (var n = r.array, i = 0; i < n.length; i++)
                                    null != n[i].ocsp && (n[i].ocsp = e)
                        }
                        ,
                        this.updateExtAIACAIssuer = function(t, e) {
                            var r = this.findExt(t, "authorityInfoAccess");
                            if (null != r && null != r.array)
                                for (var n = r.array, i = 0; i < n.length; i++)
                                    null != n[i].caissuer && (n[i].caissuer = e)
                        }
                        ,
                        this.dnarraytostr = function(t) {
                            return "/" + t.map((function(t) {
                                return function(t) {
                                    return t.map((function(t) {
                                        return function(t) {
                                            return t.type + "=" + t.value
                                        }(t)
                                    }
                                    )).join("+")
                                }(t)
                            }
                            )).join("/")
                        }
                        ,
                        this.getInfo = function() {
                            var t, e, r, n = function(t) {
                                return JSON.stringify(t.array).replace(/[\[\]\{\}\"]/g, "")
                            }, i = function(t) {
                                for (var e = "", r = t.array, n = 0; n < r.length; n++) {
                                    var i = r[n];
                                    if (e += "    policy oid: " + i.policyoid + "\n",
                                    void 0 !== i.array)
                                        for (var o = 0; o < i.array.length; o++) {
                                            var s = i.array[o];
                                            void 0 !== s.cps && (e += "    cps: " + s.cps + "\n")
                                        }
                                }
                                return e
                            }, o = function(t) {
                                for (var e = "", r = t.array, n = 0; n < r.length; n++) {
                                    var i = r[n];
                                    try {
                                        void 0 !== i.dpname.full[0].uri && (e += "    " + i.dpname.full[0].uri + "\n")
                                    } catch (t) {}
                                    try {
                                        void 0 !== i.dname.full[0].dn.hex && (e += "    " + Wt.hex2dn(i.dpname.full[0].dn.hex) + "\n")
                                    } catch (t) {}
                                }
                                return e
                            }, s = function(t) {
                                for (var e = "", r = t.array, n = 0; n < r.length; n++) {
                                    var i = r[n];
                                    void 0 !== i.caissuer && (e += "    caissuer: " + i.caissuer + "\n"),
                                    void 0 !== i.ocsp && (e += "    ocsp: " + i.ocsp + "\n")
                                }
                                return e
                            };
                            if (t = "Basic Fields\n",
                            t += "  serial number: " + this.getSerialNumberHex() + "\n",
                            t += "  signature algorithm: " + this.getSignatureAlgorithmField() + "\n",
                            t += "  issuer: " + this.getIssuerString() + "\n",
                            t += "  notBefore: " + this.getNotBefore() + "\n",
                            t += "  notAfter: " + this.getNotAfter() + "\n",
                            t += "  subject: " + this.getSubjectString() + "\n",
                            t += "  subject public key info: \n",
                            t += "    key algorithm: " + (e = this.getPublicKey()).type + "\n",
                            "RSA" === e.type && (t += "    n=" + Mt(e.n.toString(16)).substr(0, 16) + "...\n",
                            t += "    e=" + Mt(e.e.toString(16)) + "\n"),
                            null != (r = this.aExtInfo)) {
                                t += "X509v3 Extensions:\n";
                                for (var a = 0; a < r.length; a++) {
                                    var u = r[a]
                                      , c = ct.asn1.x509.OID.oid2name(u.oid);
                                    "" === c && (c = u.oid);
                                    var h = "";
                                    if (!0 === u.critical && (h = "CRITICAL"),
                                    t += "  " + c + " " + h + ":\n",
                                    "basicConstraints" === c) {
                                        var l = this.getExtBasicConstraints();
                                        void 0 === l.cA ? t += "    {}\n" : (t += "    cA=true",
                                        void 0 !== l.pathLen && (t += ", pathLen=" + l.pathLen),
                                        t += "\n")
                                    } else if ("keyUsage" === c)
                                        t += "    " + this.getExtKeyUsageString() + "\n";
                                    else if ("subjectKeyIdentifier" === c)
                                        t += "    " + this.getExtSubjectKeyIdentifier().kid.hex + "\n";
                                    else if ("authorityKeyIdentifier" === c) {
                                        var f = this.getExtAuthorityKeyIdentifier();
                                        void 0 !== f.kid && (t += "    kid=" + f.kid.hex + "\n")
                                    } else
                                        "extKeyUsage" === c ? t += "    " + this.getExtExtKeyUsage().array.join(", ") + "\n" : "subjectAltName" === c ? t += "    " + n(this.getExtSubjectAltName()) + "\n" : "cRLDistributionPoints" === c ? t += o(this.getExtCRLDistributionPoints()) : "authorityInfoAccess" === c ? t += s(this.getExtAuthorityInfoAccess()) : "certificatePolicies" === c && (t += i(this.getExtCertificatePolicies()))
                                }
                            }
                            return (t += "signature algorithm: " + this.getSignatureAlgorithmName() + "\n") + "signature: " + this.getSignatureValueHex().substr(0, 16) + "...\n"
                        }
                        ,
                        "string" == typeof t && (-1 != t.indexOf("-----BEGIN") ? this.readCertPEM(t) : ct.lang.String.isHex(t) && this.readCertHex(t))
                    }
                    it.prototype.sign = function(t, e) {
                        var r = function(t) {
                            return ct.crypto.Util.hashString(t, e)
                        }(t);
                        return this.signWithMessageHash(r, e)
                    }
                    ,
                    it.prototype.signWithMessageHash = function(t, e) {
                        var r = rt(ct.crypto.Util.getPaddedDigestInfoHex(t, e, this.n.bitLength()), 16);
                        return Vt(this.doPrivate(r).toString(16), this.n.bitLength())
                    }
                    ,
                    it.prototype.signPSS = function(t, e, r) {
                        var n = function(t) {
                            return ct.crypto.Util.hashHex(t, e)
                        }(Et(t));
                        return void 0 === r && (r = -1),
                        this.signWithMessageHashPSS(n, e, r)
                    }
                    ,
                    it.prototype.signWithMessageHashPSS = function(t, e, r) {
                        var n, i = Ft(t), o = i.length, s = this.n.bitLength() - 1, a = Math.ceil(s / 8), u = function(t) {
                            return ct.crypto.Util.hashHex(t, e)
                        };
                        if (-1 === r || void 0 === r)
                            r = o;
                        else if (-2 === r)
                            r = a - o - 2;
                        else if (r < -2)
                            throw new Error("invalid salt length");
                        if (a < o + r + 2)
                            throw new Error("data too long");
                        var c = "";
                        r > 0 && (c = new Array(r),
                        (new et).nextBytes(c),
                        c = String.fromCharCode.apply(String, c));
                        var h = Ft(u(Et("\0\0\0\0\0\0\0\0" + i + c)))
                          , l = [];
                        for (n = 0; n < a - r - o - 2; n += 1)
                            l[n] = 0;
                        var f = String.fromCharCode.apply(String, l) + "" + c
                          , g = qt(h, f.length, u)
                          , d = [];
                        for (n = 0; n < f.length; n += 1)
                            d[n] = f.charCodeAt(n) ^ g.charCodeAt(n);
                        var p = 65280 >> 8 * a - s & 255;
                        for (d[0] &= ~p,
                        n = 0; n < o; n++)
                            d.push(h.charCodeAt(n));
                        return d.push(188),
                        Vt(this.doPrivate(new F(d)).toString(16), this.n.bitLength())
                    }
                    ,
                    it.prototype.verify = function(t, e) {
                        var r = rt(e = (e = e.replace(Kt, "")).replace(/[ \n]+/g, ""), 16);
                        if (r.bitLength() > this.n.bitLength())
                            return 0;
                        var n = Jt(this.doPublic(r).toString(16).replace(/^1f+00/, ""));
                        if (0 == n.length)
                            return !1;
                        var i = n[0];
                        return n[1] == function(t) {
                            return ct.crypto.Util.hashString(t, i)
                        }(t)
                    }
                    ,
                    it.prototype.verifyWithMessageHash = function(t, e) {
                        if (e.length != Math.ceil(this.n.bitLength() / 4))
                            return !1;
                        var r = rt(e, 16);
                        if (r.bitLength() > this.n.bitLength())
                            return 0;
                        var n = Jt(this.doPublic(r).toString(16).replace(/^1f+00/, ""));
                        return 0 != n.length && (n[0],
                        n[1] == t)
                    }
                    ,
                    it.prototype.verifyPSS = function(t, e, r, n) {
                        var i = function(t) {
                            return ct.crypto.Util.hashHex(t, r)
                        }(Et(t));
                        return void 0 === n && (n = -1),
                        this.verifyWithMessageHashPSS(i, e, r, n)
                    }
                    ,
                    it.prototype.verifyWithMessageHashPSS = function(t, e, r, n) {
                        if (e.length != Math.ceil(this.n.bitLength() / 4))
                            return !1;
                        var i, o = new F(e,16), s = function(t) {
                            return ct.crypto.Util.hashHex(t, r)
                        }, a = Ft(t), u = a.length, c = this.n.bitLength() - 1, h = Math.ceil(c / 8);
                        if (-1 === n || void 0 === n)
                            n = u;
                        else if (-2 === n)
                            n = h - u - 2;
                        else if (n < -2)
                            throw new Error("invalid salt length");
                        if (h < u + n + 2)
                            throw new Error("data too long");
                        var l = this.doPublic(o).toByteArray();
                        for (i = 0; i < l.length; i += 1)
                            l[i] &= 255;
                        for (; l.length < h; )
                            l.unshift(0);
                        if (188 !== l[h - 1])
                            throw new Error("encoded message does not end in 0xbc");
                        var f = (l = String.fromCharCode.apply(String, l)).substr(0, h - u - 1)
                          , g = l.substr(f.length, u)
                          , d = 65280 >> 8 * h - c & 255;
                        if (0 != (f.charCodeAt(0) & d))
                            throw new Error("bits beyond keysize not zero");
                        var p = qt(g, f.length, s)
                          , v = [];
                        for (i = 0; i < f.length; i += 1)
                            v[i] = f.charCodeAt(i) ^ p.charCodeAt(i);
                        v[0] &= ~d;
                        var y = h - u - n - 2;
                        for (i = 0; i < y; i += 1)
                            if (0 !== v[i])
                                throw new Error("leftmost octets not zero");
                        if (1 !== v[y])
                            throw new Error("0x01 marker not found");
                        return g === Ft(s(Et("\0\0\0\0\0\0\0\0" + a + String.fromCharCode.apply(String, v.slice(-n)))))
                    }
                    ,
                    it.SALT_LEN_HLEN = -1,
                    it.SALT_LEN_MAX = -2,
                    it.SALT_LEN_RECOVER = -2,
                    Wt.hex2dn = function(t, e) {
                        if (void 0 === e && (e = 0),
                        "30" !== t.substr(e, 2))
                            throw new Error("malformed DN");
                        for (var r = new Array, n = ft.getChildIdx(t, e), i = 0; i < n.length; i++)
                            r.push(Wt.hex2rdn(t, n[i]));
                        return "/" + (r = r.map((function(t) {
                            return t.replace("/", "\\/")
                        }
                        ))).join("/")
                    }
                    ,
                    Wt.hex2rdn = function(t, e) {
                        if (void 0 === e && (e = 0),
                        "31" !== t.substr(e, 2))
                            throw new Error("malformed RDN");
                        for (var r = new Array, n = ft.getChildIdx(t, e), i = 0; i < n.length; i++)
                            r.push(Wt.hex2attrTypeValue(t, n[i]));
                        return (r = r.map((function(t) {
                            return t.replace("+", "\\+")
                        }
                        ))).join("+")
                    }
                    ,
                    Wt.hex2attrTypeValue = function(t, e) {
                        var r = ft
                          , n = r.getV;
                        if (void 0 === e && (e = 0),
                        "30" !== t.substr(e, 2))
                            throw new Error("malformed attribute type and value");
                        var i = r.getChildIdx(t, e);
                        2 !== i.length || t.substr(i[0], 2);
                        var o = n(t, i[0])
                          , s = ct.asn1.ASN1Util.oidHexToInt(o);
                        return ct.asn1.x509.OID.oid2atype(s) + "=" + Ft(n(t, i[1]))
                    }
                    ,
                    Wt.getPublicKeyFromCertHex = function(t) {
                        var e = new Wt;
                        return e.readCertHex(t),
                        e.getPublicKey()
                    }
                    ,
                    Wt.getPublicKeyFromCertPEM = function(t) {
                        var e = new Wt;
                        return e.readCertPEM(t),
                        e.getPublicKey()
                    }
                    ,
                    Wt.getPublicKeyInfoPropOfCertPEM = function(t) {
                        var e, r, n = ft.getVbyList, i = {
                            algparam: null
                        };
                        return (e = new Wt).readCertPEM(t),
                        r = e.getPublicKeyHex(),
                        i.keyhex = n(r, 0, [1], "03").substr(2),
                        i.algoid = n(r, 0, [0, 0], "06"),
                        "2a8648ce3d0201" === i.algoid && (i.algparam = n(r, 0, [0, 1], "06")),
                        i
                    }
                    ,
                    Wt.KEYUSAGE_NAME = ["digitalSignature", "nonRepudiation", "keyEncipherment", "dataEncipherment", "keyAgreement", "keyCertSign", "cRLSign", "encipherOnly", "decipherOnly"],
                    void 0 !== ct && ct || (e.KJUR = ct = {}),
                    void 0 !== ct.jws && ct.jws || (ct.jws = {}),
                    ct.jws.JWS = function() {
                        var t = ct.jws.JWS.isSafeJSONString;
                        this.parseJWS = function(e, r) {
                            if (void 0 === this.parsedJWS || !r && void 0 === this.parsedJWS.sigvalH) {
                                var n = e.match(/^([^.]+)\.([^.]+)\.([^.]+)$/);
                                if (null == n)
                                    throw "JWS signature is not a form of 'Head.Payload.SigValue'.";
                                var i = n[1]
                                  , o = n[2]
                                  , s = n[3]
                                  , a = i + "." + o;
                                if (this.parsedJWS = {},
                                this.parsedJWS.headB64U = i,
                                this.parsedJWS.payloadB64U = o,
                                this.parsedJWS.sigvalB64U = s,
                                this.parsedJWS.si = a,
                                !r) {
                                    var u = St(s)
                                      , c = rt(u, 16);
                                    this.parsedJWS.sigvalH = u,
                                    this.parsedJWS.sigvalBI = c
                                }
                                var h = lt(i)
                                  , l = lt(o);
                                if (this.parsedJWS.headS = h,
                                this.parsedJWS.payloadS = l,
                                !t(h, this.parsedJWS, "headP"))
                                    throw "malformed JSON string for JWS Head: " + h
                            }
                        }
                    }
                    ,
                    ct.jws.JWS.sign = function(t, e, n, i, o) {
                        var s, a, u, c = ct, h = c.jws.JWS, l = h.readSafeJSONString, f = h.isSafeJSONString, g = c.crypto, d = (g.ECDSA,
                        g.Mac), p = g.Signature, v = JSON;
                        if ("string" != typeof e && "object" != (void 0 === e ? "undefined" : r(e)))
                            throw "spHeader must be JSON string or object: " + e;
                        if ("object" == (void 0 === e ? "undefined" : r(e)) && (a = e,
                        s = v.stringify(a)),
                        "string" == typeof e) {
                            if (!f(s = e))
                                throw "JWS Head is not safe JSON string: " + s;
                            a = l(s)
                        }
                        if (u = n,
                        "object" == (void 0 === n ? "undefined" : r(n)) && (u = v.stringify(n)),
                        "" != t && null != t || void 0 === a.alg || (t = a.alg),
                        "" != t && null != t && void 0 === a.alg && (a.alg = t,
                        s = v.stringify(a)),
                        t !== a.alg)
                            throw "alg and sHeader.alg doesn't match: " + t + "!=" + a.alg;
                        var y = null;
                        if (void 0 === h.jwsalg2sigalg[t])
                            throw "unsupported alg name: " + t;
                        y = h.jwsalg2sigalg[t];
                        var m = ht(s) + "." + ht(u)
                          , _ = "";
                        if ("Hmac" == y.substr(0, 4)) {
                            if (void 0 === i)
                                throw "mac key shall be specified for HS* alg";
                            var S = new d({
                                alg: y,
                                prov: "cryptojs",
                                pass: i
                            });
                            S.updateString(m),
                            _ = S.doFinal()
                        } else if (-1 != y.indexOf("withECDSA")) {
                            (b = new p({
                                alg: y
                            })).init(i, o),
                            b.updateString(m);
                            var w = b.sign();
                            _ = ct.crypto.ECDSA.asn1SigToConcatSig(w)
                        } else {
                            var b;
                            "none" != y && ((b = new p({
                                alg: y
                            })).init(i, o),
                            b.updateString(m),
                            _ = b.sign())
                        }
                        return m + "." + _t(_)
                    }
                    ,
                    ct.jws.JWS.verify = function(t, e, n) {
                        var i, o = ct, s = o.jws.JWS, a = s.readSafeJSONString, u = o.crypto, c = u.ECDSA, h = u.Mac, l = u.Signature;
                        void 0 !== r(it) && (i = it);
                        var f = t.split(".");
                        if (3 !== f.length)
                            return !1;
                        var g, d = f[0] + "." + f[1], p = St(f[2]), v = a(lt(f[0])), y = null;
                        if (void 0 === v.alg)
                            throw "algorithm not specified in header";
                        if (g = (y = v.alg).substr(0, 2),
                        null != n && "[object Array]" === Object.prototype.toString.call(n) && n.length > 0 && -1 == (":" + n.join(":") + ":").indexOf(":" + y + ":"))
                            throw "algorithm '" + y + "' not accepted in the list";
                        if ("none" != y && null === e)
                            throw "key shall be specified to verify.";
                        if ("string" == typeof e && -1 != e.indexOf("-----BEGIN ") && (e = Ht.getKey(e)),
                        !("RS" != g && "PS" != g || e instanceof i))
                            throw "key shall be a RSAKey obj for RS* and PS* algs";
                        if ("ES" == g && !(e instanceof c))
                            throw "key shall be a ECDSA obj for ES* algs";
                        var m = null;
                        if (void 0 === s.jwsalg2sigalg[v.alg])
                            throw "unsupported alg name: " + y;
                        if ("none" == (m = s.jwsalg2sigalg[y]))
                            throw "not supported";
                        if ("Hmac" == m.substr(0, 4)) {
                            if (void 0 === e)
                                throw "hexadecimal key shall be specified for HMAC";
                            var _ = new h({
                                alg: m,
                                pass: e
                            });
                            return _.updateString(d),
                            p == _.doFinal()
                        }
                        if (-1 != m.indexOf("withECDSA")) {
                            var S, w = null;
                            try {
                                w = c.concatSigToASN1Sig(p)
                            } catch (t) {
                                return !1
                            }
                            return (S = new l({
                                alg: m
                            })).init(e),
                            S.updateString(d),
                            S.verify(w)
                        }
                        return (S = new l({
                            alg: m
                        })).init(e),
                        S.updateString(d),
                        S.verify(p)
                    }
                    ,
                    ct.jws.JWS.parse = function(t) {
                        var e, r, n, i = t.split("."), o = {};
                        if (2 != i.length && 3 != i.length)
                            throw "malformed sJWS: wrong number of '.' splitted elements";
                        return e = i[0],
                        r = i[1],
                        3 == i.length && (n = i[2]),
                        o.headerObj = ct.jws.JWS.readSafeJSONString(lt(e)),
                        o.payloadObj = ct.jws.JWS.readSafeJSONString(lt(r)),
                        o.headerPP = JSON.stringify(o.headerObj, null, "  "),
                        null == o.payloadObj ? o.payloadPP = lt(r) : o.payloadPP = JSON.stringify(o.payloadObj, null, "  "),
                        void 0 !== n && (o.sigHex = St(n)),
                        o
                    }
                    ,
                    ct.jws.JWS.verifyJWT = function(t, e, n) {
                        var i = ct.jws
                          , o = i.JWS
                          , s = o.readSafeJSONString
                          , a = o.inArray
                          , u = o.includedArray
                          , c = t.split(".")
                          , h = c[0]
                          , l = c[1]
                          , f = (St(c[2]),
                        s(lt(h)))
                          , g = s(lt(l));
                        if (void 0 === f.alg)
                            return !1;
                        if (void 0 === n.alg)
                            throw "acceptField.alg shall be specified";
                        if (!a(f.alg, n.alg))
                            return !1;
                        if (void 0 !== g.iss && "object" === r(n.iss) && !a(g.iss, n.iss))
                            return !1;
                        if (void 0 !== g.sub && "object" === r(n.sub) && !a(g.sub, n.sub))
                            return !1;
                        if (void 0 !== g.aud && "object" === r(n.aud))
                            if ("string" == typeof g.aud) {
                                if (!a(g.aud, n.aud))
                                    return !1
                            } else if ("object" == r(g.aud) && !u(g.aud, n.aud))
                                return !1;
                        var d = i.IntDate.getNow();
                        return void 0 !== n.verifyAt && "number" == typeof n.verifyAt && (d = n.verifyAt),
                        void 0 !== n.gracePeriod && "number" == typeof n.gracePeriod || (n.gracePeriod = 0),
                        !(void 0 !== g.exp && "number" == typeof g.exp && g.exp + n.gracePeriod < d || void 0 !== g.nbf && "number" == typeof g.nbf && d < g.nbf - n.gracePeriod || void 0 !== g.iat && "number" == typeof g.iat && d < g.iat - n.gracePeriod || void 0 !== g.jti && void 0 !== n.jti && g.jti !== n.jti || !o.verify(t, e, n.alg))
                    }
                    ,
                    ct.jws.JWS.includedArray = function(t, e) {
                        var n = ct.jws.JWS.inArray;
                        if (null === t)
                            return !1;
                        if ("object" !== (void 0 === t ? "undefined" : r(t)))
                            return !1;
                        if ("number" != typeof t.length)
                            return !1;
                        for (var i = 0; i < t.length; i++)
                            if (!n(t[i], e))
                                return !1;
                        return !0
                    }
                    ,
                    ct.jws.JWS.inArray = function(t, e) {
                        if (null === e)
                            return !1;
                        if ("object" !== (void 0 === e ? "undefined" : r(e)))
                            return !1;
                        if ("number" != typeof e.length)
                            return !1;
                        for (var n = 0; n < e.length; n++)
                            if (e[n] == t)
                                return !0;
                        return !1
                    }
                    ,
                    ct.jws.JWS.jwsalg2sigalg = {
                        HS256: "HmacSHA256",
                        HS384: "HmacSHA384",
                        HS512: "HmacSHA512",
                        RS256: "SHA256withRSA",
                        RS384: "SHA384withRSA",
                        RS512: "SHA512withRSA",
                        ES256: "SHA256withECDSA",
                        ES384: "SHA384withECDSA",
                        PS256: "SHA256withRSAandMGF1",
                        PS384: "SHA384withRSAandMGF1",
                        PS512: "SHA512withRSAandMGF1",
                        none: "none"
                    },
                    ct.jws.JWS.isSafeJSONString = function(t, e, n) {
                        var i = null;
                        try {
                            return "object" != (void 0 === (i = ut(t)) ? "undefined" : r(i)) || i.constructor === Array ? 0 : (e && (e[n] = i),
                            1)
                        } catch (t) {
                            return 0
                        }
                    }
                    ,
                    ct.jws.JWS.readSafeJSONString = function(t) {
                        var e = null;
                        try {
                            return "object" != (void 0 === (e = ut(t)) ? "undefined" : r(e)) || e.constructor === Array ? null : e
                        } catch (t) {
                            return null
                        }
                    }
                    ,
                    ct.jws.JWS.getEncodedSignatureValueFromJWS = function(t) {
                        var e = t.match(/^[^.]+\.[^.]+\.([^.]+)$/);
                        if (null == e)
                            throw "JWS signature is not a form of 'Head.Payload.SigValue'.";
                        return e[1]
                    }
                    ,
                    ct.jws.JWS.getJWKthumbprint = function(t) {
                        if ("RSA" !== t.kty && "EC" !== t.kty && "oct" !== t.kty)
                            throw "unsupported algorithm for JWK Thumprint";
                        var e = "{";
                        if ("RSA" === t.kty) {
                            if ("string" != typeof t.n || "string" != typeof t.e)
                                throw "wrong n and e value for RSA key";
                            e += '"e":"' + t.e + '",',
                            e += '"kty":"' + t.kty + '",',
                            e += '"n":"' + t.n + '"}'
                        } else if ("EC" === t.kty) {
                            if ("string" != typeof t.crv || "string" != typeof t.x || "string" != typeof t.y)
                                throw "wrong crv, x and y value for EC key";
                            e += '"crv":"' + t.crv + '",',
                            e += '"kty":"' + t.kty + '",',
                            e += '"x":"' + t.x + '",',
                            e += '"y":"' + t.y + '"}'
                        } else if ("oct" === t.kty) {
                            if ("string" != typeof t.k)
                                throw "wrong k value for oct(symmetric) key";
                            e += '"kty":"' + t.kty + '",',
                            e += '"k":"' + t.k + '"}'
                        }
                        var r = Et(e);
                        return _t(ct.crypto.Util.hashHex(r, "sha256"))
                    }
                    ,
                    ct.jws.IntDate = {},
                    ct.jws.IntDate.get = function(t) {
                        var e = ct.jws.IntDate
                          , r = e.getNow
                          , n = e.getZulu;
                        if ("now" == t)
                            return r();
                        if ("now + 1hour" == t)
                            return r() + 3600;
                        if ("now + 1day" == t)
                            return r() + 86400;
                        if ("now + 1month" == t)
                            return r() + 2592e3;
                        if ("now + 1year" == t)
                            return r() + 31536e3;
                        if (t.match(/Z$/))
                            return n(t);
                        if (t.match(/^[0-9]+$/))
                            return parseInt(t);
                        throw "unsupported format: " + t
                    }
                    ,
                    ct.jws.IntDate.getZulu = function(t) {
                        return Rt(t)
                    }
                    ,
                    ct.jws.IntDate.getNow = function() {
                        return ~~(new Date / 1e3)
                    }
                    ,
                    ct.jws.IntDate.intDate2UTCString = function(t) {
                        return new Date(1e3 * t).toUTCString()
                    }
                    ,
                    ct.jws.IntDate.intDate2Zulu = function(t) {
                        var e = new Date(1e3 * t);
                        return ("0000" + e.getUTCFullYear()).slice(-4) + ("00" + (e.getUTCMonth() + 1)).slice(-2) + ("00" + e.getUTCDate()).slice(-2) + ("00" + e.getUTCHours()).slice(-2) + ("00" + e.getUTCMinutes()).slice(-2) + ("00" + e.getUTCSeconds()).slice(-2) + "Z"
                    }
                    ,
                    e.SecureRandom = et,
                    e.rng_seed_time = G,
                    e.BigInteger = F,
                    e.RSAKey = it;
                    var zt = ct.crypto.EDSA;
                    e.EDSA = zt;
                    var Yt = ct.crypto.DSA;
                    e.DSA = Yt;
                    var Gt = ct.crypto.Signature;
                    e.Signature = Gt;
                    var Xt = ct.crypto.MessageDigest;
                    e.MessageDigest = Xt;
                    var $t = ct.crypto.Mac;
                    e.Mac = $t;
                    var Qt = ct.crypto.Cipher;
                    e.Cipher = Qt,
                    e.KEYUTIL = Ht,
                    e.ASN1HEX = ft,
                    e.X509 = Wt,
                    e.CryptoJS = y,
                    e.b64tohex = w,
                    e.b64toBA = b,
                    e.stoBA = gt,
                    e.BAtos = dt,
                    e.BAtohex = pt,
                    e.stohex = vt,
                    e.stob64 = function(t) {
                        return S(vt(t))
                    }
                    ,
                    e.stob64u = function(t) {
                        return yt(S(vt(t)))
                    }
                    ,
                    e.b64utos = function(t) {
                        return dt(b(mt(t)))
                    }
                    ,
                    e.b64tob64u = yt,
                    e.b64utob64 = mt,
                    e.hex2b64 = S,
                    e.hextob64u = _t,
                    e.b64utohex = St,
                    e.utf8tob64u = ht,
                    e.b64utoutf8 = lt,
                    e.utf8tob64 = function(t) {
                        return S(It(Ot(t)))
                    }
                    ,
                    e.b64toutf8 = function(t) {
                        return decodeURIComponent(Dt(w(t)))
                    }
                    ,
                    e.utf8tohex = wt,
                    e.hextoutf8 = bt,
                    e.hextorstr = Ft,
                    e.rstrtohex = Et,
                    e.hextob64 = xt,
                    e.hextob64nl = At,
                    e.b64nltohex = kt,
                    e.hextopem = Pt,
                    e.pemtohex = Ct,
                    e.hextoArrayBuffer = function(t) {
                        if (t.length % 2 != 0)
                            throw "input is not even length";
                        if (null == t.match(/^[0-9A-Fa-f]+$/))
                            throw "input is not hexadecimal";
                        for (var e = new ArrayBuffer(t.length / 2), r = new DataView(e), n = 0; n < t.length / 2; n++)
                            r.setUint8(n, parseInt(t.substr(2 * n, 2), 16));
                        return e
                    }
                    ,
                    e.ArrayBuffertohex = function(t) {
                        for (var e = "", r = new DataView(t), n = 0; n < t.byteLength; n++)
                            e += ("00" + r.getUint8(n).toString(16)).slice(-2);
                        return e
                    }
                    ,
                    e.zulutomsec = Tt,
                    e.zulutosec = Rt,
                    e.zulutodate = function(t) {
                        return new Date(Tt(t))
                    }
                    ,
                    e.datetozulu = function(t, e, r) {
                        var n, i = t.getUTCFullYear();
                        if (e) {
                            if (i < 1950 || 2049 < i)
                                throw "not proper year for UTCTime: " + i;
                            n = ("" + i).slice(-2)
                        } else
                            n = ("000" + i).slice(-4);
                        if (n += ("0" + (t.getUTCMonth() + 1)).slice(-2),
                        n += ("0" + t.getUTCDate()).slice(-2),
                        n += ("0" + t.getUTCHours()).slice(-2),
                        n += ("0" + t.getUTCMinutes()).slice(-2),
                        n += ("0" + t.getUTCSeconds()).slice(-2),
                        r) {
                            var o = t.getUTCMilliseconds();
                            0 !== o && (n += "." + (o = (o = ("00" + o).slice(-3)).replace(/0+$/g, "")))
                        }
                        return n + "Z"
                    }
                    ,
                    e.uricmptohex = It,
                    e.hextouricmp = Dt,
                    e.ipv6tohex = Lt,
                    e.hextoipv6 = Nt,
                    e.hextoip = Ut,
                    e.iptohex = function(t) {
                        var e = "malformed IP address";
                        if (!(t = t.toLowerCase(t)).match(/^[0-9.]+$/)) {
                            if (t.match(/^[0-9a-f:]+$/) && -1 !== t.indexOf(":"))
                                return Lt(t);
                            throw e
                        }
                        var r = t.split(".");
                        if (4 !== r.length)
                            throw e;
                        var n = "";
                        try {
                            for (var i = 0; i < 4; i++)
                                n += ("0" + parseInt(r[i]).toString(16)).slice(-2);
                            return n
                        } catch (t) {
                            throw e
                        }
                    }
                    ,
                    e.encodeURIComponentAll = Ot,
                    e.newline_toUnix = function(t) {
                        return t.replace(/\r\n/gm, "\n")
                    }
                    ,
                    e.newline_toDos = function(t) {
                        return (t = t.replace(/\r\n/gm, "\n")).replace(/\n/gm, "\r\n")
                    }
                    ,
                    e.hextoposhex = Mt,
                    e.intarystrtohex = function(t) {
                        t = (t = (t = t.replace(/^\s*\[\s*/, "")).replace(/\s*\]\s*$/, "")).replace(/\s*/g, "");
                        try {
                            return t.split(/,/).map((function(t, e, r) {
                                var n = parseInt(t);
                                if (n < 0 || 255 < n)
                                    throw "integer not in range 0-255";
                                return ("00" + n.toString(16)).slice(-2)
                            }
                            )).join("")
                        } catch (t) {
                            throw "malformed integer array string: " + t
                        }
                    }
                    ,
                    e.strdiffidx = function(t, e) {
                        var r = t.length;
                        t.length > e.length && (r = e.length);
                        for (var n = 0; n < r; n++)
                            if (t.charCodeAt(n) != e.charCodeAt(n))
                                return n;
                        return t.length != e.length ? r : -1
                    }
                    ,
                    e.KJUR = ct;
                    var Zt = ct.crypto;
                    e.crypto = Zt;
                    var te = ct.asn1;
                    e.asn1 = te;
                    var ee = ct.jws;
                    e.jws = ee;
                    var re = ct.lang;
                    e.lang = re
                }
                ).call(this, r(28).Buffer)
            }
            , function(t, e, r) {
                "use strict";
                (function(t) {
                    var n = r(30)
                      , i = r(31)
                      , o = r(32);
                    function s() {
                        return u.TYPED_ARRAY_SUPPORT ? 2147483647 : 1073741823
                    }
                    function a(t, e) {
                        if (s() < e)
                            throw new RangeError("Invalid typed array length");
                        return u.TYPED_ARRAY_SUPPORT ? (t = new Uint8Array(e)).__proto__ = u.prototype : (null === t && (t = new u(e)),
                        t.length = e),
                        t
                    }
                    function u(t, e, r) {
                        if (!(u.TYPED_ARRAY_SUPPORT || this instanceof u))
                            return new u(t,e,r);
                        if ("number" == typeof t) {
                            if ("string" == typeof e)
                                throw new Error("If encoding is specified then the first argument must be a string");
                            return l(this, t)
                        }
                        return c(this, t, e, r)
                    }
                    function c(t, e, r, n) {
                        if ("number" == typeof e)
                            throw new TypeError('"value" argument must not be a number');
                        return "undefined" != typeof ArrayBuffer && e instanceof ArrayBuffer ? function(t, e, r, n) {
                            if (e.byteLength,
                            r < 0 || e.byteLength < r)
                                throw new RangeError("'offset' is out of bounds");
                            if (e.byteLength < r + (n || 0))
                                throw new RangeError("'length' is out of bounds");
                            return e = void 0 === r && void 0 === n ? new Uint8Array(e) : void 0 === n ? new Uint8Array(e,r) : new Uint8Array(e,r,n),
                            u.TYPED_ARRAY_SUPPORT ? (t = e).__proto__ = u.prototype : t = f(t, e),
                            t
                        }(t, e, r, n) : "string" == typeof e ? function(t, e, r) {
                            if ("string" == typeof r && "" !== r || (r = "utf8"),
                            !u.isEncoding(r))
                                throw new TypeError('"encoding" must be a valid string encoding');
                            var n = 0 | d(e, r)
                              , i = (t = a(t, n)).write(e, r);
                            return i !== n && (t = t.slice(0, i)),
                            t
                        }(t, e, r) : function(t, e) {
                            if (u.isBuffer(e)) {
                                var r = 0 | g(e.length);
                                return 0 === (t = a(t, r)).length || e.copy(t, 0, 0, r),
                                t
                            }
                            if (e) {
                                if ("undefined" != typeof ArrayBuffer && e.buffer instanceof ArrayBuffer || "length"in e)
                                    return "number" != typeof e.length || function(t) {
                                        return t != t
                                    }(e.length) ? a(t, 0) : f(t, e);
                                if ("Buffer" === e.type && o(e.data))
                                    return f(t, e.data)
                            }
                            throw new TypeError("First argument must be a string, Buffer, ArrayBuffer, Array, or array-like object.")
                        }(t, e)
                    }
                    function h(t) {
                        if ("number" != typeof t)
                            throw new TypeError('"size" argument must be a number');
                        if (t < 0)
                            throw new RangeError('"size" argument must not be negative')
                    }
                    function l(t, e) {
                        if (h(e),
                        t = a(t, e < 0 ? 0 : 0 | g(e)),
                        !u.TYPED_ARRAY_SUPPORT)
                            for (var r = 0; r < e; ++r)
                                t[r] = 0;
                        return t
                    }
                    function f(t, e) {
                        var r = e.length < 0 ? 0 : 0 | g(e.length);
                        t = a(t, r);
                        for (var n = 0; n < r; n += 1)
                            t[n] = 255 & e[n];
                        return t
                    }
                    function g(t) {
                        if (t >= s())
                            throw new RangeError("Attempt to allocate Buffer larger than maximum size: 0x" + s().toString(16) + " bytes");
                        return 0 | t
                    }
                    function d(t, e) {
                        if (u.isBuffer(t))
                            return t.length;
                        if ("undefined" != typeof ArrayBuffer && "function" == typeof ArrayBuffer.isView && (ArrayBuffer.isView(t) || t instanceof ArrayBuffer))
                            return t.byteLength;
                        "string" != typeof t && (t = "" + t);
                        var r = t.length;
                        if (0 === r)
                            return 0;
                        for (var n = !1; ; )
                            switch (e) {
                            case "ascii":
                            case "latin1":
                            case "binary":
                                return r;
                            case "utf8":
                            case "utf-8":
                            case void 0:
                                return H(t).length;
                            case "ucs2":
                            case "ucs-2":
                            case "utf16le":
                            case "utf-16le":
                                return 2 * r;
                            case "hex":
                                return r >>> 1;
                            case "base64":
                                return K(t).length;
                            default:
                                if (n)
                                    return H(t).length;
                                e = ("" + e).toLowerCase(),
                                n = !0
                            }
                    }
                    function p(t, e, r) {
                        var n = !1;
                        if ((void 0 === e || e < 0) && (e = 0),
                        e > this.length)
                            return "";
                        if ((void 0 === r || r > this.length) && (r = this.length),
                        r <= 0)
                            return "";
                        if ((r >>>= 0) <= (e >>>= 0))
                            return "";
                        for (t || (t = "utf8"); ; )
                            switch (t) {
                            case "hex":
                                return T(this, e, r);
                            case "utf8":
                            case "utf-8":
                                return A(this, e, r);
                            case "ascii":
                                return P(this, e, r);
                            case "latin1":
                            case "binary":
                                return C(this, e, r);
                            case "base64":
                                return x(this, e, r);
                            case "ucs2":
                            case "ucs-2":
                            case "utf16le":
                            case "utf-16le":
                                return R(this, e, r);
                            default:
                                if (n)
                                    throw new TypeError("Unknown encoding: " + t);
                                t = (t + "").toLowerCase(),
                                n = !0
                            }
                    }
                    function v(t, e, r) {
                        var n = t[e];
                        t[e] = t[r],
                        t[r] = n
                    }
                    function y(t, e, r, n, i) {
                        if (0 === t.length)
                            return -1;
                        if ("string" == typeof r ? (n = r,
                        r = 0) : r > 2147483647 ? r = 2147483647 : r < -2147483648 && (r = -2147483648),
                        r = +r,
                        isNaN(r) && (r = i ? 0 : t.length - 1),
                        r < 0 && (r = t.length + r),
                        r >= t.length) {
                            if (i)
                                return -1;
                            r = t.length - 1
                        } else if (r < 0) {
                            if (!i)
                                return -1;
                            r = 0
                        }
                        if ("string" == typeof e && (e = u.from(e, n)),
                        u.isBuffer(e))
                            return 0 === e.length ? -1 : m(t, e, r, n, i);
                        if ("number" == typeof e)
                            return e &= 255,
                            u.TYPED_ARRAY_SUPPORT && "function" == typeof Uint8Array.prototype.indexOf ? i ? Uint8Array.prototype.indexOf.call(t, e, r) : Uint8Array.prototype.lastIndexOf.call(t, e, r) : m(t, [e], r, n, i);
                        throw new TypeError("val must be string, number or Buffer")
                    }
                    function m(t, e, r, n, i) {
                        var o, s = 1, a = t.length, u = e.length;
                        if (void 0 !== n && ("ucs2" === (n = String(n).toLowerCase()) || "ucs-2" === n || "utf16le" === n || "utf-16le" === n)) {
                            if (t.length < 2 || e.length < 2)
                                return -1;
                            s = 2,
                            a /= 2,
                            u /= 2,
                            r /= 2
                        }
                        function c(t, e) {
                            return 1 === s ? t[e] : t.readUInt16BE(e * s)
                        }
                        if (i) {
                            var h = -1;
                            for (o = r; o < a; o++)
                                if (c(t, o) === c(e, -1 === h ? 0 : o - h)) {
                                    if (-1 === h && (h = o),
                                    o - h + 1 === u)
                                        return h * s
                                } else
                                    -1 !== h && (o -= o - h),
                                    h = -1
                        } else
                            for (r + u > a && (r = a - u),
                            o = r; o >= 0; o--) {
                                for (var l = !0, f = 0; f < u; f++)
                                    if (c(t, o + f) !== c(e, f)) {
                                        l = !1;
                                        break
                                    }
                                if (l)
                                    return o
                            }
                        return -1
                    }
                    function _(t, e, r, n) {
                        r = Number(r) || 0;
                        var i = t.length - r;
                        n ? (n = Number(n)) > i && (n = i) : n = i;
                        var o = e.length;
                        if (o % 2 != 0)
                            throw new TypeError("Invalid hex string");
                        n > o / 2 && (n = o / 2);
                        for (var s = 0; s < n; ++s) {
                            var a = parseInt(e.substr(2 * s, 2), 16);
                            if (isNaN(a))
                                return s;
                            t[r + s] = a
                        }
                        return s
                    }
                    function S(t, e, r, n) {
                        return V(H(e, t.length - r), t, r, n)
                    }
                    function w(t, e, r, n) {
                        return V(function(t) {
                            for (var e = [], r = 0; r < t.length; ++r)
                                e.push(255 & t.charCodeAt(r));
                            return e
                        }(e), t, r, n)
                    }
                    function b(t, e, r, n) {
                        return w(t, e, r, n)
                    }
                    function F(t, e, r, n) {
                        return V(K(e), t, r, n)
                    }
                    function E(t, e, r, n) {
                        return V(function(t, e) {
                            for (var r, n, i, o = [], s = 0; s < t.length && !((e -= 2) < 0); ++s)
                                n = (r = t.charCodeAt(s)) >> 8,
                                i = r % 256,
                                o.push(i),
                                o.push(n);
                            return o
                        }(e, t.length - r), t, r, n)
                    }
                    function x(t, e, r) {
                        return 0 === e && r === t.length ? n.fromByteArray(t) : n.fromByteArray(t.slice(e, r))
                    }
                    function A(t, e, r) {
                        r = Math.min(t.length, r);
                        for (var n = [], i = e; i < r; ) {
                            var o, s, a, u, c = t[i], h = null, l = c > 239 ? 4 : c > 223 ? 3 : c > 191 ? 2 : 1;
                            if (i + l <= r)
                                switch (l) {
                                case 1:
                                    c < 128 && (h = c);
                                    break;
                                case 2:
                                    128 == (192 & (o = t[i + 1])) && (u = (31 & c) << 6 | 63 & o) > 127 && (h = u);
                                    break;
                                case 3:
                                    o = t[i + 1],
                                    s = t[i + 2],
                                    128 == (192 & o) && 128 == (192 & s) && (u = (15 & c) << 12 | (63 & o) << 6 | 63 & s) > 2047 && (u < 55296 || u > 57343) && (h = u);
                                    break;
                                case 4:
                                    o = t[i + 1],
                                    s = t[i + 2],
                                    a = t[i + 3],
                                    128 == (192 & o) && 128 == (192 & s) && 128 == (192 & a) && (u = (15 & c) << 18 | (63 & o) << 12 | (63 & s) << 6 | 63 & a) > 65535 && u < 1114112 && (h = u)
                                }
                            null === h ? (h = 65533,
                            l = 1) : h > 65535 && (h -= 65536,
                            n.push(h >>> 10 & 1023 | 55296),
                            h = 56320 | 1023 & h),
                            n.push(h),
                            i += l
                        }
                        return function(t) {
                            var e = t.length;
                            if (e <= k)
                                return String.fromCharCode.apply(String, t);
                            for (var r = "", n = 0; n < e; )
                                r += String.fromCharCode.apply(String, t.slice(n, n += k));
                            return r
                        }(n)
                    }
                    e.Buffer = u,
                    e.SlowBuffer = function(t) {
                        return +t != t && (t = 0),
                        u.alloc(+t)
                    }
                    ,
                    e.INSPECT_MAX_BYTES = 50,
                    u.TYPED_ARRAY_SUPPORT = void 0 !== t.TYPED_ARRAY_SUPPORT ? t.TYPED_ARRAY_SUPPORT : function() {
                        try {
                            var t = new Uint8Array(1);
                            return t.__proto__ = {
                                __proto__: Uint8Array.prototype,
                                foo: function() {
                                    return 42
                                }
                            },
                            42 === t.foo() && "function" == typeof t.subarray && 0 === t.subarray(1, 1).byteLength
                        } catch (t) {
                            return !1
                        }
                    }(),
                    e.kMaxLength = s(),
                    u.poolSize = 8192,
                    u._augment = function(t) {
                        return t.__proto__ = u.prototype,
                        t
                    }
                    ,
                    u.from = function(t, e, r) {
                        return c(null, t, e, r)
                    }
                    ,
                    u.TYPED_ARRAY_SUPPORT && (u.prototype.__proto__ = Uint8Array.prototype,
                    u.__proto__ = Uint8Array,
                    "undefined" != typeof Symbol && Symbol.species && u[Symbol.species] === u && Object.defineProperty(u, Symbol.species, {
                        value: null,
                        configurable: !0
                    })),
                    u.alloc = function(t, e, r) {
                        return function(t, e, r, n) {
                            return h(e),
                            e <= 0 ? a(t, e) : void 0 !== r ? "string" == typeof n ? a(t, e).fill(r, n) : a(t, e).fill(r) : a(t, e)
                        }(null, t, e, r)
                    }
                    ,
                    u.allocUnsafe = function(t) {
                        return l(null, t)
                    }
                    ,
                    u.allocUnsafeSlow = function(t) {
                        return l(null, t)
                    }
                    ,
                    u.isBuffer = function(t) {
                        return !(null == t || !t._isBuffer)
                    }
                    ,
                    u.compare = function(t, e) {
                        if (!u.isBuffer(t) || !u.isBuffer(e))
                            throw new TypeError("Arguments must be Buffers");
                        if (t === e)
                            return 0;
                        for (var r = t.length, n = e.length, i = 0, o = Math.min(r, n); i < o; ++i)
                            if (t[i] !== e[i]) {
                                r = t[i],
                                n = e[i];
                                break
                            }
                        return r < n ? -1 : n < r ? 1 : 0
                    }
                    ,
                    u.isEncoding = function(t) {
                        switch (String(t).toLowerCase()) {
                        case "hex":
                        case "utf8":
                        case "utf-8":
                        case "ascii":
                        case "latin1":
                        case "binary":
                        case "base64":
                        case "ucs2":
                        case "ucs-2":
                        case "utf16le":
                        case "utf-16le":
                            return !0;
                        default:
                            return !1
                        }
                    }
                    ,
                    u.concat = function(t, e) {
                        if (!o(t))
                            throw new TypeError('"list" argument must be an Array of Buffers');
                        if (0 === t.length)
                            return u.alloc(0);
                        var r;
                        if (void 0 === e)
                            for (e = 0,
                            r = 0; r < t.length; ++r)
                                e += t[r].length;
                        var n = u.allocUnsafe(e)
                          , i = 0;
                        for (r = 0; r < t.length; ++r) {
                            var s = t[r];
                            if (!u.isBuffer(s))
                                throw new TypeError('"list" argument must be an Array of Buffers');
                            s.copy(n, i),
                            i += s.length
                        }
                        return n
                    }
                    ,
                    u.byteLength = d,
                    u.prototype._isBuffer = !0,
                    u.prototype.swap16 = function() {
                        var t = this.length;
                        if (t % 2 != 0)
                            throw new RangeError("Buffer size must be a multiple of 16-bits");
                        for (var e = 0; e < t; e += 2)
                            v(this, e, e + 1);
                        return this
                    }
                    ,
                    u.prototype.swap32 = function() {
                        var t = this.length;
                        if (t % 4 != 0)
                            throw new RangeError("Buffer size must be a multiple of 32-bits");
                        for (var e = 0; e < t; e += 4)
                            v(this, e, e + 3),
                            v(this, e + 1, e + 2);
                        return this
                    }
                    ,
                    u.prototype.swap64 = function() {
                        var t = this.length;
                        if (t % 8 != 0)
                            throw new RangeError("Buffer size must be a multiple of 64-bits");
                        for (var e = 0; e < t; e += 8)
                            v(this, e, e + 7),
                            v(this, e + 1, e + 6),
                            v(this, e + 2, e + 5),
                            v(this, e + 3, e + 4);
                        return this
                    }
                    ,
                    u.prototype.toString = function() {
                        var t = 0 | this.length;
                        return 0 === t ? "" : 0 === arguments.length ? A(this, 0, t) : p.apply(this, arguments)
                    }
                    ,
                    u.prototype.equals = function(t) {
                        if (!u.isBuffer(t))
                            throw new TypeError("Argument must be a Buffer");
                        return this === t || 0 === u.compare(this, t)
                    }
                    ,
                    u.prototype.inspect = function() {
                        var t = ""
                          , r = e.INSPECT_MAX_BYTES;
                        return this.length > 0 && (t = this.toString("hex", 0, r).match(/.{2}/g).join(" "),
                        this.length > r && (t += " ... ")),
                        "<Buffer " + t + ">"
                    }
                    ,
                    u.prototype.compare = function(t, e, r, n, i) {
                        if (!u.isBuffer(t))
                            throw new TypeError("Argument must be a Buffer");
                        if (void 0 === e && (e = 0),
                        void 0 === r && (r = t ? t.length : 0),
                        void 0 === n && (n = 0),
                        void 0 === i && (i = this.length),
                        e < 0 || r > t.length || n < 0 || i > this.length)
                            throw new RangeError("out of range index");
                        if (n >= i && e >= r)
                            return 0;
                        if (n >= i)
                            return -1;
                        if (e >= r)
                            return 1;
                        if (this === t)
                            return 0;
                        for (var o = (i >>>= 0) - (n >>>= 0), s = (r >>>= 0) - (e >>>= 0), a = Math.min(o, s), c = this.slice(n, i), h = t.slice(e, r), l = 0; l < a; ++l)
                            if (c[l] !== h[l]) {
                                o = c[l],
                                s = h[l];
                                break
                            }
                        return o < s ? -1 : s < o ? 1 : 0
                    }
                    ,
                    u.prototype.includes = function(t, e, r) {
                        return -1 !== this.indexOf(t, e, r)
                    }
                    ,
                    u.prototype.indexOf = function(t, e, r) {
                        return y(this, t, e, r, !0)
                    }
                    ,
                    u.prototype.lastIndexOf = function(t, e, r) {
                        return y(this, t, e, r, !1)
                    }
                    ,
                    u.prototype.write = function(t, e, r, n) {
                        if (void 0 === e)
                            n = "utf8",
                            r = this.length,
                            e = 0;
                        else if (void 0 === r && "string" == typeof e)
                            n = e,
                            r = this.length,
                            e = 0;
                        else {
                            if (!isFinite(e))
                                throw new Error("Buffer.write(string, encoding, offset[, length]) is no longer supported");
                            e |= 0,
                            isFinite(r) ? (r |= 0,
                            void 0 === n && (n = "utf8")) : (n = r,
                            r = void 0)
                        }
                        var i = this.length - e;
                        if ((void 0 === r || r > i) && (r = i),
                        t.length > 0 && (r < 0 || e < 0) || e > this.length)
                            throw new RangeError("Attempt to write outside buffer bounds");
                        n || (n = "utf8");
                        for (var o = !1; ; )
                            switch (n) {
                            case "hex":
                                return _(this, t, e, r);
                            case "utf8":
                            case "utf-8":
                                return S(this, t, e, r);
                            case "ascii":
                                return w(this, t, e, r);
                            case "latin1":
                            case "binary":
                                return b(this, t, e, r);
                            case "base64":
                                return F(this, t, e, r);
                            case "ucs2":
                            case "ucs-2":
                            case "utf16le":
                            case "utf-16le":
                                return E(this, t, e, r);
                            default:
                                if (o)
                                    throw new TypeError("Unknown encoding: " + n);
                                n = ("" + n).toLowerCase(),
                                o = !0
                            }
                    }
                    ,
                    u.prototype.toJSON = function() {
                        return {
                            type: "Buffer",
                            data: Array.prototype.slice.call(this._arr || this, 0)
                        }
                    }
                    ;
                    var k = 4096;
                    function P(t, e, r) {
                        var n = "";
                        r = Math.min(t.length, r);
                        for (var i = e; i < r; ++i)
                            n += String.fromCharCode(127 & t[i]);
                        return n
                    }
                    function C(t, e, r) {
                        var n = "";
                        r = Math.min(t.length, r);
                        for (var i = e; i < r; ++i)
                            n += String.fromCharCode(t[i]);
                        return n
                    }
                    function T(t, e, r) {
                        var n = t.length;
                        (!e || e < 0) && (e = 0),
                        (!r || r < 0 || r > n) && (r = n);
                        for (var i = "", o = e; o < r; ++o)
                            i += j(t[o]);
                        return i
                    }
                    function R(t, e, r) {
                        for (var n = t.slice(e, r), i = "", o = 0; o < n.length; o += 2)
                            i += String.fromCharCode(n[o] + 256 * n[o + 1]);
                        return i
                    }
                    function I(t, e, r) {
                        if (t % 1 != 0 || t < 0)
                            throw new RangeError("offset is not uint");
                        if (t + e > r)
                            throw new RangeError("Trying to access beyond buffer length")
                    }
                    function D(t, e, r, n, i, o) {
                        if (!u.isBuffer(t))
                            throw new TypeError('"buffer" argument must be a Buffer instance');
                        if (e > i || e < o)
                            throw new RangeError('"value" argument is out of bounds');
                        if (r + n > t.length)
                            throw new RangeError("Index out of range")
                    }
                    function L(t, e, r, n) {
                        e < 0 && (e = 65535 + e + 1);
                        for (var i = 0, o = Math.min(t.length - r, 2); i < o; ++i)
                            t[r + i] = (e & 255 << 8 * (n ? i : 1 - i)) >>> 8 * (n ? i : 1 - i)
                    }
                    function N(t, e, r, n) {
                        e < 0 && (e = 4294967295 + e + 1);
                        for (var i = 0, o = Math.min(t.length - r, 4); i < o; ++i)
                            t[r + i] = e >>> 8 * (n ? i : 3 - i) & 255
                    }
                    function U(t, e, r, n, i, o) {
                        if (r + n > t.length)
                            throw new RangeError("Index out of range");
                        if (r < 0)
                            throw new RangeError("Index out of range")
                    }
                    function O(t, e, r, n, o) {
                        return o || U(t, 0, r, 4),
                        i.write(t, e, r, n, 23, 4),
                        r + 4
                    }
                    function B(t, e, r, n, o) {
                        return o || U(t, 0, r, 8),
                        i.write(t, e, r, n, 52, 8),
                        r + 8
                    }
                    u.prototype.slice = function(t, e) {
                        var r, n = this.length;
                        if ((t = ~~t) < 0 ? (t += n) < 0 && (t = 0) : t > n && (t = n),
                        (e = void 0 === e ? n : ~~e) < 0 ? (e += n) < 0 && (e = 0) : e > n && (e = n),
                        e < t && (e = t),
                        u.TYPED_ARRAY_SUPPORT)
                            (r = this.subarray(t, e)).__proto__ = u.prototype;
                        else {
                            var i = e - t;
                            r = new u(i,void 0);
                            for (var o = 0; o < i; ++o)
                                r[o] = this[o + t]
                        }
                        return r
                    }
                    ,
                    u.prototype.readUIntLE = function(t, e, r) {
                        t |= 0,
                        e |= 0,
                        r || I(t, e, this.length);
                        for (var n = this[t], i = 1, o = 0; ++o < e && (i *= 256); )
                            n += this[t + o] * i;
                        return n
                    }
                    ,
                    u.prototype.readUIntBE = function(t, e, r) {
                        t |= 0,
                        e |= 0,
                        r || I(t, e, this.length);
                        for (var n = this[t + --e], i = 1; e > 0 && (i *= 256); )
                            n += this[t + --e] * i;
                        return n
                    }
                    ,
                    u.prototype.readUInt8 = function(t, e) {
                        return e || I(t, 1, this.length),
                        this[t]
                    }
                    ,
                    u.prototype.readUInt16LE = function(t, e) {
                        return e || I(t, 2, this.length),
                        this[t] | this[t + 1] << 8
                    }
                    ,
                    u.prototype.readUInt16BE = function(t, e) {
                        return e || I(t, 2, this.length),
                        this[t] << 8 | this[t + 1]
                    }
                    ,
                    u.prototype.readUInt32LE = function(t, e) {
                        return e || I(t, 4, this.length),
                        (this[t] | this[t + 1] << 8 | this[t + 2] << 16) + 16777216 * this[t + 3]
                    }
                    ,
                    u.prototype.readUInt32BE = function(t, e) {
                        return e || I(t, 4, this.length),
                        16777216 * this[t] + (this[t + 1] << 16 | this[t + 2] << 8 | this[t + 3])
                    }
                    ,
                    u.prototype.readIntLE = function(t, e, r) {
                        t |= 0,
                        e |= 0,
                        r || I(t, e, this.length);
                        for (var n = this[t], i = 1, o = 0; ++o < e && (i *= 256); )
                            n += this[t + o] * i;
                        return n >= (i *= 128) && (n -= Math.pow(2, 8 * e)),
                        n
                    }
                    ,
                    u.prototype.readIntBE = function(t, e, r) {
                        t |= 0,
                        e |= 0,
                        r || I(t, e, this.length);
                        for (var n = e, i = 1, o = this[t + --n]; n > 0 && (i *= 256); )
                            o += this[t + --n] * i;
                        return o >= (i *= 128) && (o -= Math.pow(2, 8 * e)),
                        o
                    }
                    ,
                    u.prototype.readInt8 = function(t, e) {
                        return e || I(t, 1, this.length),
                        128 & this[t] ? -1 * (255 - this[t] + 1) : this[t]
                    }
                    ,
                    u.prototype.readInt16LE = function(t, e) {
                        e || I(t, 2, this.length);
                        var r = this[t] | this[t + 1] << 8;
                        return 32768 & r ? 4294901760 | r : r
                    }
                    ,
                    u.prototype.readInt16BE = function(t, e) {
                        e || I(t, 2, this.length);
                        var r = this[t + 1] | this[t] << 8;
                        return 32768 & r ? 4294901760 | r : r
                    }
                    ,
                    u.prototype.readInt32LE = function(t, e) {
                        return e || I(t, 4, this.length),
                        this[t] | this[t + 1] << 8 | this[t + 2] << 16 | this[t + 3] << 24
                    }
                    ,
                    u.prototype.readInt32BE = function(t, e) {
                        return e || I(t, 4, this.length),
                        this[t] << 24 | this[t + 1] << 16 | this[t + 2] << 8 | this[t + 3]
                    }
                    ,
                    u.prototype.readFloatLE = function(t, e) {
                        return e || I(t, 4, this.length),
                        i.read(this, t, !0, 23, 4)
                    }
                    ,
                    u.prototype.readFloatBE = function(t, e) {
                        return e || I(t, 4, this.length),
                        i.read(this, t, !1, 23, 4)
                    }
                    ,
                    u.prototype.readDoubleLE = function(t, e) {
                        return e || I(t, 8, this.length),
                        i.read(this, t, !0, 52, 8)
                    }
                    ,
                    u.prototype.readDoubleBE = function(t, e) {
                        return e || I(t, 8, this.length),
                        i.read(this, t, !1, 52, 8)
                    }
                    ,
                    u.prototype.writeUIntLE = function(t, e, r, n) {
                        t = +t,
                        e |= 0,
                        r |= 0,
                        n || D(this, t, e, r, Math.pow(2, 8 * r) - 1, 0);
                        var i = 1
                          , o = 0;
                        for (this[e] = 255 & t; ++o < r && (i *= 256); )
                            this[e + o] = t / i & 255;
                        return e + r
                    }
                    ,
                    u.prototype.writeUIntBE = function(t, e, r, n) {
                        t = +t,
                        e |= 0,
                        r |= 0,
                        n || D(this, t, e, r, Math.pow(2, 8 * r) - 1, 0);
                        var i = r - 1
                          , o = 1;
                        for (this[e + i] = 255 & t; --i >= 0 && (o *= 256); )
                            this[e + i] = t / o & 255;
                        return e + r
                    }
                    ,
                    u.prototype.writeUInt8 = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 1, 255, 0),
                        u.TYPED_ARRAY_SUPPORT || (t = Math.floor(t)),
                        this[e] = 255 & t,
                        e + 1
                    }
                    ,
                    u.prototype.writeUInt16LE = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 2, 65535, 0),
                        u.TYPED_ARRAY_SUPPORT ? (this[e] = 255 & t,
                        this[e + 1] = t >>> 8) : L(this, t, e, !0),
                        e + 2
                    }
                    ,
                    u.prototype.writeUInt16BE = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 2, 65535, 0),
                        u.TYPED_ARRAY_SUPPORT ? (this[e] = t >>> 8,
                        this[e + 1] = 255 & t) : L(this, t, e, !1),
                        e + 2
                    }
                    ,
                    u.prototype.writeUInt32LE = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 4, 4294967295, 0),
                        u.TYPED_ARRAY_SUPPORT ? (this[e + 3] = t >>> 24,
                        this[e + 2] = t >>> 16,
                        this[e + 1] = t >>> 8,
                        this[e] = 255 & t) : N(this, t, e, !0),
                        e + 4
                    }
                    ,
                    u.prototype.writeUInt32BE = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 4, 4294967295, 0),
                        u.TYPED_ARRAY_SUPPORT ? (this[e] = t >>> 24,
                        this[e + 1] = t >>> 16,
                        this[e + 2] = t >>> 8,
                        this[e + 3] = 255 & t) : N(this, t, e, !1),
                        e + 4
                    }
                    ,
                    u.prototype.writeIntLE = function(t, e, r, n) {
                        if (t = +t,
                        e |= 0,
                        !n) {
                            var i = Math.pow(2, 8 * r - 1);
                            D(this, t, e, r, i - 1, -i)
                        }
                        var o = 0
                          , s = 1
                          , a = 0;
                        for (this[e] = 255 & t; ++o < r && (s *= 256); )
                            t < 0 && 0 === a && 0 !== this[e + o - 1] && (a = 1),
                            this[e + o] = (t / s >> 0) - a & 255;
                        return e + r
                    }
                    ,
                    u.prototype.writeIntBE = function(t, e, r, n) {
                        if (t = +t,
                        e |= 0,
                        !n) {
                            var i = Math.pow(2, 8 * r - 1);
                            D(this, t, e, r, i - 1, -i)
                        }
                        var o = r - 1
                          , s = 1
                          , a = 0;
                        for (this[e + o] = 255 & t; --o >= 0 && (s *= 256); )
                            t < 0 && 0 === a && 0 !== this[e + o + 1] && (a = 1),
                            this[e + o] = (t / s >> 0) - a & 255;
                        return e + r
                    }
                    ,
                    u.prototype.writeInt8 = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 1, 127, -128),
                        u.TYPED_ARRAY_SUPPORT || (t = Math.floor(t)),
                        t < 0 && (t = 255 + t + 1),
                        this[e] = 255 & t,
                        e + 1
                    }
                    ,
                    u.prototype.writeInt16LE = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 2, 32767, -32768),
                        u.TYPED_ARRAY_SUPPORT ? (this[e] = 255 & t,
                        this[e + 1] = t >>> 8) : L(this, t, e, !0),
                        e + 2
                    }
                    ,
                    u.prototype.writeInt16BE = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 2, 32767, -32768),
                        u.TYPED_ARRAY_SUPPORT ? (this[e] = t >>> 8,
                        this[e + 1] = 255 & t) : L(this, t, e, !1),
                        e + 2
                    }
                    ,
                    u.prototype.writeInt32LE = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 4, 2147483647, -2147483648),
                        u.TYPED_ARRAY_SUPPORT ? (this[e] = 255 & t,
                        this[e + 1] = t >>> 8,
                        this[e + 2] = t >>> 16,
                        this[e + 3] = t >>> 24) : N(this, t, e, !0),
                        e + 4
                    }
                    ,
                    u.prototype.writeInt32BE = function(t, e, r) {
                        return t = +t,
                        e |= 0,
                        r || D(this, t, e, 4, 2147483647, -2147483648),
                        t < 0 && (t = 4294967295 + t + 1),
                        u.TYPED_ARRAY_SUPPORT ? (this[e] = t >>> 24,
                        this[e + 1] = t >>> 16,
                        this[e + 2] = t >>> 8,
                        this[e + 3] = 255 & t) : N(this, t, e, !1),
                        e + 4
                    }
                    ,
                    u.prototype.writeFloatLE = function(t, e, r) {
                        return O(this, t, e, !0, r)
                    }
                    ,
                    u.prototype.writeFloatBE = function(t, e, r) {
                        return O(this, t, e, !1, r)
                    }
                    ,
                    u.prototype.writeDoubleLE = function(t, e, r) {
                        return B(this, t, e, !0, r)
                    }
                    ,
                    u.prototype.writeDoubleBE = function(t, e, r) {
                        return B(this, t, e, !1, r)
                    }
                    ,
                    u.prototype.copy = function(t, e, r, n) {
                        if (r || (r = 0),
                        n || 0 === n || (n = this.length),
                        e >= t.length && (e = t.length),
                        e || (e = 0),
                        n > 0 && n < r && (n = r),
                        n === r)
                            return 0;
                        if (0 === t.length || 0 === this.length)
                            return 0;
                        if (e < 0)
                            throw new RangeError("targetStart out of bounds");
                        if (r < 0 || r >= this.length)
                            throw new RangeError("sourceStart out of bounds");
                        if (n < 0)
                            throw new RangeError("sourceEnd out of bounds");
                        n > this.length && (n = this.length),
                        t.length - e < n - r && (n = t.length - e + r);
                        var i, o = n - r;
                        if (this === t && r < e && e < n)
                            for (i = o - 1; i >= 0; --i)
                                t[i + e] = this[i + r];
                        else if (o < 1e3 || !u.TYPED_ARRAY_SUPPORT)
                            for (i = 0; i < o; ++i)
                                t[i + e] = this[i + r];
                        else
                            Uint8Array.prototype.set.call(t, this.subarray(r, r + o), e);
                        return o
                    }
                    ,
                    u.prototype.fill = function(t, e, r, n) {
                        if ("string" == typeof t) {
                            if ("string" == typeof e ? (n = e,
                            e = 0,
                            r = this.length) : "string" == typeof r && (n = r,
                            r = this.length),
                            1 === t.length) {
                                var i = t.charCodeAt(0);
                                i < 256 && (t = i)
                            }
                            if (void 0 !== n && "string" != typeof n)
                                throw new TypeError("encoding must be a string");
                            if ("string" == typeof n && !u.isEncoding(n))
                                throw new TypeError("Unknown encoding: " + n)
                        } else
                            "number" == typeof t && (t &= 255);
                        if (e < 0 || this.length < e || this.length < r)
                            throw new RangeError("Out of range index");
                        if (r <= e)
                            return this;
                        var o;
                        if (e >>>= 0,
                        r = void 0 === r ? this.length : r >>> 0,
                        t || (t = 0),
                        "number" == typeof t)
                            for (o = e; o < r; ++o)
                                this[o] = t;
                        else {
                            var s = u.isBuffer(t) ? t : H(new u(t,n).toString())
                              , a = s.length;
                            for (o = 0; o < r - e; ++o)
                                this[o + e] = s[o % a]
                        }
                        return this
                    }
                    ;
                    var M = /[^+\/0-9A-Za-z-_]/g;
                    function j(t) {
                        return t < 16 ? "0" + t.toString(16) : t.toString(16)
                    }
                    function H(t, e) {
                        var r;
                        e = e || 1 / 0;
                        for (var n = t.length, i = null, o = [], s = 0; s < n; ++s) {
                            if ((r = t.charCodeAt(s)) > 55295 && r < 57344) {
                                if (!i) {
                                    if (r > 56319) {
                                        (e -= 3) > -1 && o.push(239, 191, 189);
                                        continue
                                    }
                                    if (s + 1 === n) {
                                        (e -= 3) > -1 && o.push(239, 191, 189);
                                        continue
                                    }
                                    i = r;
                                    continue
                                }
                                if (r < 56320) {
                                    (e -= 3) > -1 && o.push(239, 191, 189),
                                    i = r;
                                    continue
                                }
                                r = 65536 + (i - 55296 << 10 | r - 56320)
                            } else
                                i && (e -= 3) > -1 && o.push(239, 191, 189);
                            if (i = null,
                            r < 128) {
                                if ((e -= 1) < 0)
                                    break;
                                o.push(r)
                            } else if (r < 2048) {
                                if ((e -= 2) < 0)
                                    break;
                                o.push(r >> 6 | 192, 63 & r | 128)
                            } else if (r < 65536) {
                                if ((e -= 3) < 0)
                                    break;
                                o.push(r >> 12 | 224, r >> 6 & 63 | 128, 63 & r | 128)
                            } else {
                                if (!(r < 1114112))
                                    throw new Error("Invalid code point");
                                if ((e -= 4) < 0)
                                    break;
                                o.push(r >> 18 | 240, r >> 12 & 63 | 128, r >> 6 & 63 | 128, 63 & r | 128)
                            }
                        }
                        return o
                    }
                    function K(t) {
                        return n.toByteArray(function(t) {
                            if ((t = function(t) {
                                return t.trim ? t.trim() : t.replace(/^\s+|\s+$/g, "")
                            }(t).replace(M, "")).length < 2)
                                return "";
                            for (; t.length % 4 != 0; )
                                t += "=";
                            return t
                        }(t))
                    }
                    function V(t, e, r, n) {
                        for (var i = 0; i < n && !(i + r >= e.length || i >= t.length); ++i)
                            e[i + r] = t[i];
                        return i
                    }
                }
                ).call(this, r(29))
            }
            , function(t, e) {
                var r;
                r = function() {
                    return this
                }();
                try {
                    r = r || new Function("return this")()
                } catch (t) {
                    "object" == typeof window && (r = window)
                }
                t.exports = r
            }
            , function(t, e, r) {
                "use strict";
                e.byteLength = function(t) {
                    var e = c(t)
                      , r = e[0]
                      , n = e[1];
                    return 3 * (r + n) / 4 - n
                }
                ,
                e.toByteArray = function(t) {
                    var e, r, n = c(t), s = n[0], a = n[1], u = new o(function(t, e, r) {
                        return 3 * (e + r) / 4 - r
                    }(0, s, a)), h = 0, l = a > 0 ? s - 4 : s;
                    for (r = 0; r < l; r += 4)
                        e = i[t.charCodeAt(r)] << 18 | i[t.charCodeAt(r + 1)] << 12 | i[t.charCodeAt(r + 2)] << 6 | i[t.charCodeAt(r + 3)],
                        u[h++] = e >> 16 & 255,
                        u[h++] = e >> 8 & 255,
                        u[h++] = 255 & e;
                    return 2 === a && (e = i[t.charCodeAt(r)] << 2 | i[t.charCodeAt(r + 1)] >> 4,
                    u[h++] = 255 & e),
                    1 === a && (e = i[t.charCodeAt(r)] << 10 | i[t.charCodeAt(r + 1)] << 4 | i[t.charCodeAt(r + 2)] >> 2,
                    u[h++] = e >> 8 & 255,
                    u[h++] = 255 & e),
                    u
                }
                ,
                e.fromByteArray = function(t) {
                    for (var e, r = t.length, i = r % 3, o = [], s = 16383, a = 0, u = r - i; a < u; a += s)
                        o.push(h(t, a, a + s > u ? u : a + s));
                    return 1 === i ? (e = t[r - 1],
                    o.push(n[e >> 2] + n[e << 4 & 63] + "==")) : 2 === i && (e = (t[r - 2] << 8) + t[r - 1],
                    o.push(n[e >> 10] + n[e >> 4 & 63] + n[e << 2 & 63] + "=")),
                    o.join("")
                }
                ;
                for (var n = [], i = [], o = "undefined" != typeof Uint8Array ? Uint8Array : Array, s = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/", a = 0, u = s.length; a < u; ++a)
                    n[a] = s[a],
                    i[s.charCodeAt(a)] = a;
                function c(t) {
                    var e = t.length;
                    if (e % 4 > 0)
                        throw new Error("Invalid string. Length must be a multiple of 4");
                    var r = t.indexOf("=");
                    return -1 === r && (r = e),
                    [r, r === e ? 0 : 4 - r % 4]
                }
                function h(t, e, r) {
                    for (var i, o, s = [], a = e; a < r; a += 3)
                        i = (t[a] << 16 & 16711680) + (t[a + 1] << 8 & 65280) + (255 & t[a + 2]),
                        s.push(n[(o = i) >> 18 & 63] + n[o >> 12 & 63] + n[o >> 6 & 63] + n[63 & o]);
                    return s.join("")
                }
                i["-".charCodeAt(0)] = 62,
                i["_".charCodeAt(0)] = 63
            }
            , function(t, e) {
                e.read = function(t, e, r, n, i) {
                    var o, s, a = 8 * i - n - 1, u = (1 << a) - 1, c = u >> 1, h = -7, l = r ? i - 1 : 0, f = r ? -1 : 1, g = t[e + l];
                    for (l += f,
                    o = g & (1 << -h) - 1,
                    g >>= -h,
                    h += a; h > 0; o = 256 * o + t[e + l],
                    l += f,
                    h -= 8)
                        ;
                    for (s = o & (1 << -h) - 1,
                    o >>= -h,
                    h += n; h > 0; s = 256 * s + t[e + l],
                    l += f,
                    h -= 8)
                        ;
                    if (0 === o)
                        o = 1 - c;
                    else {
                        if (o === u)
                            return s ? NaN : 1 / 0 * (g ? -1 : 1);
                        s += Math.pow(2, n),
                        o -= c
                    }
                    return (g ? -1 : 1) * s * Math.pow(2, o - n)
                }
                ,
                e.write = function(t, e, r, n, i, o) {
                    var s, a, u, c = 8 * o - i - 1, h = (1 << c) - 1, l = h >> 1, f = 23 === i ? Math.pow(2, -24) - Math.pow(2, -77) : 0, g = n ? 0 : o - 1, d = n ? 1 : -1, p = e < 0 || 0 === e && 1 / e < 0 ? 1 : 0;
                    for (e = Math.abs(e),
                    isNaN(e) || e === 1 / 0 ? (a = isNaN(e) ? 1 : 0,
                    s = h) : (s = Math.floor(Math.log(e) / Math.LN2),
                    e * (u = Math.pow(2, -s)) < 1 && (s--,
                    u *= 2),
                    (e += s + l >= 1 ? f / u : f * Math.pow(2, 1 - l)) * u >= 2 && (s++,
                    u /= 2),
                    s + l >= h ? (a = 0,
                    s = h) : s + l >= 1 ? (a = (e * u - 1) * Math.pow(2, i),
                    s += l) : (a = e * Math.pow(2, l - 1) * Math.pow(2, i),
                    s = 0)); i >= 8; t[r + g] = 255 & a,
                    g += d,
                    a /= 256,
                    i -= 8)
                        ;
                    for (s = s << i | a,
                    c += i; c > 0; t[r + g] = 255 & s,
                    g += d,
                    s /= 256,
                    c -= 8)
                        ;
                    t[r + g - d] |= 128 * p
                }
            }
            , function(t, e) {
                var r = {}.toString;
                t.exports = Array.isArray || function(t) {
                    return "[object Array]" == r.call(t)
                }
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.default = function(t) {
                    var e = t.jws
                      , r = t.KeyUtil
                      , i = t.X509
                      , o = t.crypto
                      , s = t.hextob64u
                      , a = t.b64tohex
                      , u = t.AllowedSigningAlgs;
                    return function() {
                        function t() {
                            !function(t, e) {
                                if (!(t instanceof e))
                                    throw new TypeError("Cannot call a class as a function")
                            }(this, t)
                        }
                        return t.parseJwt = function t(r) {
                            n.Log.debug("JoseUtil.parseJwt");
                            try {
                                var i = e.JWS.parse(r);
                                return {
                                    header: i.headerObj,
                                    payload: i.payloadObj
                                }
                            } catch (t) {
                                n.Log.error(t)
                            }
                        }
                        ,
                        t.validateJwt = function(e, o, s, u, c, h, l) {
                            n.Log.debug("JoseUtil.validateJwt");
                            try {
                                if ("RSA" === o.kty)
                                    if (o.e && o.n)
                                        o = r.getKey(o);
                                    else {
                                        if (!o.x5c || !o.x5c.length)
                                            return n.Log.error("JoseUtil.validateJwt: RSA key missing key material", o),
                                            Promise.reject(new Error("RSA key missing key material"));
                                        var f = a(o.x5c[0]);
                                        o = i.getPublicKeyFromCertHex(f)
                                    }
                                else {
                                    if ("EC" !== o.kty)
                                        return n.Log.error("JoseUtil.validateJwt: Unsupported key type", o && o.kty),
                                        Promise.reject(new Error(o.kty));
                                    if (!(o.crv && o.x && o.y))
                                        return n.Log.error("JoseUtil.validateJwt: EC key missing key material", o),
                                        Promise.reject(new Error("EC key missing key material"));
                                    o = r.getKey(o)
                                }
                                return t._validateJwt(e, o, s, u, c, h, l)
                            } catch (t) {
                                return n.Log.error(t && t.message || t),
                                Promise.reject("JWT validation failed")
                            }
                        }
                        ,
                        t.validateJwtAttributes = function(e, r, i, o, s, a) {
                            o || (o = 0),
                            s || (s = parseInt(Date.now() / 1e3));
                            var u = t.parseJwt(e).payload;
                            if (!u.iss)
                                return n.Log.error("JoseUtil._validateJwt: issuer was not provided"),
                                Promise.reject(new Error("issuer was not provided"));
                            if (u.iss !== r)
                                return n.Log.error("JoseUtil._validateJwt: Invalid issuer in token", u.iss),
                                Promise.reject(new Error("Invalid issuer in token: " + u.iss));
                            if (!u.aud)
                                return n.Log.error("JoseUtil._validateJwt: aud was not provided"),
                                Promise.reject(new Error("aud was not provided"));
                            if (!(u.aud === i || Array.isArray(u.aud) && u.aud.indexOf(i) >= 0))
                                return n.Log.error("JoseUtil._validateJwt: Invalid audience in token", u.aud),
                                Promise.reject(new Error("Invalid audience in token: " + u.aud));
                            if (u.azp && u.azp !== i)
                                return n.Log.error("JoseUtil._validateJwt: Invalid azp in token", u.azp),
                                Promise.reject(new Error("Invalid azp in token: " + u.azp));
                            if (!a) {
                                var c = s + o
                                  , h = s - o;
                                if (!u.iat)
                                    return n.Log.error("JoseUtil._validateJwt: iat was not provided"),
                                    Promise.reject(new Error("iat was not provided"));
                                if (c < u.iat)
                                    return n.Log.error("JoseUtil._validateJwt: iat is in the future", u.iat),
                                    Promise.reject(new Error("iat is in the future: " + u.iat));
                                if (u.nbf && c < u.nbf)
                                    return n.Log.error("JoseUtil._validateJwt: nbf is in the future", u.nbf),
                                    Promise.reject(new Error("nbf is in the future: " + u.nbf));
                                if (!u.exp)
                                    return n.Log.error("JoseUtil._validateJwt: exp was not provided"),
                                    Promise.reject(new Error("exp was not provided"));
                                if (u.exp < h)
                                    return n.Log.error("JoseUtil._validateJwt: exp is in the past", u.exp),
                                    Promise.reject(new Error("exp is in the past:" + u.exp))
                            }
                            return Promise.resolve(u)
                        }
                        ,
                        t._validateJwt = function(r, i, o, s, a, c, h) {
                            return t.validateJwtAttributes(r, o, s, a, c, h).then((function(t) {
                                try {
                                    return e.JWS.verify(r, i, u) ? t : (n.Log.error("JoseUtil._validateJwt: signature validation failed"),
                                    Promise.reject(new Error("signature validation failed")))
                                } catch (t) {
                                    return n.Log.error(t && t.message || t),
                                    Promise.reject(new Error("signature validation failed"))
                                }
                            }
                            ))
                        }
                        ,
                        t.hashString = function t(e, r) {
                            try {
                                return o.Util.hashString(e, r)
                            } catch (t) {
                                n.Log.error(t)
                            }
                        }
                        ,
                        t.hexToBase64Url = function t(e) {
                            try {
                                return s(e)
                            } catch (t) {
                                n.Log.error(t)
                            }
                        }
                        ,
                        t
                    }()
                }
                ;
                var n = r(0);
                t.exports = e.default
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.SigninResponse = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(3);
                function o(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                e.SigninResponse = function() {
                    function t(e) {
                        var r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "#";
                        o(this, t);
                        var n = i.UrlUtility.parseUrlFragment(e, r);
                        this.error = n.error,
                        this.error_description = n.error_description,
                        this.error_uri = n.error_uri,
                        this.code = n.code,
                        this.state = n.state,
                        this.id_token = n.id_token,
                        this.session_state = n.session_state,
                        this.access_token = n.access_token,
                        this.token_type = n.token_type,
                        this.scope = n.scope,
                        this.profile = void 0,
                        this.expires_in = n.expires_in
                    }
                    return n(t, [{
                        key: "expires_in",
                        get: function() {
                            if (this.expires_at) {
                                var t = parseInt(Date.now() / 1e3);
                                return this.expires_at - t
                            }
                        },
                        set: function(t) {
                            var e = parseInt(t);
                            if ("number" == typeof e && e > 0) {
                                var r = parseInt(Date.now() / 1e3);
                                this.expires_at = r + e
                            }
                        }
                    }, {
                        key: "expired",
                        get: function() {
                            var t = this.expires_in;
                            if (void 0 !== t)
                                return t <= 0
                        }
                    }, {
                        key: "scopes",
                        get: function() {
                            return (this.scope || "").split(" ")
                        }
                    }, {
                        key: "isOpenIdConnect",
                        get: function() {
                            return this.scopes.indexOf("openid") >= 0 || !!this.id_token
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.SignoutRequest = void 0;
                var n = r(0)
                  , i = r(3)
                  , o = r(9);
                e.SignoutRequest = function t(e) {
                    var r = e.url
                      , s = e.id_token_hint
                      , a = e.post_logout_redirect_uri
                      , u = e.data
                      , c = e.extraQueryParams
                      , h = e.request_type;
                    if (function(t, e) {
                        if (!(t instanceof e))
                            throw new TypeError("Cannot call a class as a function")
                    }(this, t),
                    !r)
                        throw n.Log.error("SignoutRequest.ctor: No url passed"),
                        new Error("url");
                    for (var l in s && (r = i.UrlUtility.addQueryParam(r, "id_token_hint", s)),
                    //PATCH: Patch logout_uri and client_id to make logout compliant with cognito requirements, hardcoded client_id
                    a && (r = i.UrlUtility.addQueryParam(r, "logout_uri", a),
                        (r = i.UrlUtility.addQueryParam(r, "client_id", "4e3iqhodqo124t9mmt60lh72ju")),
                    u && (this.state = new o.State({
                        data: u,
                        request_type: h
                    }),
                    r = i.UrlUtility.addQueryParam(r, "state", this.state.id))),
                    c)
                        r = i.UrlUtility.addQueryParam(r, l, c[l]);
                    this.url = r
                }
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.SignoutResponse = void 0;
                var n = r(3);
                e.SignoutResponse = function t(e) {
                    !function(t, e) {
                        if (!(t instanceof e))
                            throw new TypeError("Cannot call a class as a function")
                    }(this, t);
                    var r = n.UrlUtility.parseUrlFragment(e, "?");
                    this.error = r.error,
                    this.error_description = r.error_description,
                    this.error_uri = r.error_uri,
                    this.state = r.state
                }
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.InMemoryWebStorage = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0);
                e.InMemoryWebStorage = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t),
                        this._data = {}
                    }
                    return t.prototype.getItem = function(t) {
                        return i.Log.debug("InMemoryWebStorage.getItem", t),
                        this._data[t]
                    }
                    ,
                    t.prototype.setItem = function(t, e) {
                        i.Log.debug("InMemoryWebStorage.setItem", t),
                        this._data[t] = e
                    }
                    ,
                    t.prototype.removeItem = function(t) {
                        i.Log.debug("InMemoryWebStorage.removeItem", t),
                        delete this._data[t]
                    }
                    ,
                    t.prototype.key = function(t) {
                        return Object.getOwnPropertyNames(this._data)[t]
                    }
                    ,
                    n(t, [{
                        key: "length",
                        get: function() {
                            return Object.getOwnPropertyNames(this._data).length
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.UserManager = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0)
                  , o = r(10)
                  , s = r(39)
                  , a = r(15)
                  , u = r(45)
                  , c = r(47)
                  , h = r(18)
                  , l = r(8)
                  , f = r(20)
                  , g = r(11)
                  , d = r(4);
                function p(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                function v(t, e) {
                    if (!t)
                        throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                    return !e || "object" != typeof e && "function" != typeof e ? t : e
                }
                e.UserManager = function(t) {
                    function e() {
                        var r = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : c.SilentRenewService
                          , o = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : h.SessionMonitor
                          , a = arguments.length > 3 && void 0 !== arguments[3] ? arguments[3] : f.TokenRevocationClient
                          , l = arguments.length > 4 && void 0 !== arguments[4] ? arguments[4] : g.TokenClient
                          , y = arguments.length > 5 && void 0 !== arguments[5] ? arguments[5] : d.JoseUtil;
                        p(this, e),
                        r instanceof s.UserManagerSettings || (r = new s.UserManagerSettings(r));
                        var m = v(this, t.call(this, r));
                        return m._events = new u.UserManagerEvents(r),
                        m._silentRenewService = new n(m),
                        m.settings.automaticSilentRenew && (i.Log.debug("UserManager.ctor: automaticSilentRenew is configured, setting up silent renew"),
                        m.startSilentRenew()),
                        m.settings.monitorSession && (i.Log.debug("UserManager.ctor: monitorSession is configured, setting up session monitor"),
                        m._sessionMonitor = new o(m)),
                        m._tokenRevocationClient = new a(m._settings),
                        m._tokenClient = new l(m._settings),
                        m._joseUtil = y,
                        m
                    }
                    return function(t, e) {
                        if ("function" != typeof e && null !== e)
                            throw new TypeError("Super expression must either be null or a function, not " + typeof e);
                        t.prototype = Object.create(e && e.prototype, {
                            constructor: {
                                value: t,
                                enumerable: !1,
                                writable: !0,
                                configurable: !0
                            }
                        }),
                        e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
                    }(e, t),
                    e.prototype.getUser = function() {
                        var t = this;
                        return this._loadUser().then((function(e) {
                            return e ? (i.Log.info("UserManager.getUser: user loaded"),
                            t._events.load(e, !1),
                            e) : (i.Log.info("UserManager.getUser: user not found in storage"),
                            null)
                        }
                        ))
                    }
                    ,
                    e.prototype.removeUser = function() {
                        var t = this;
                        return this.storeUser(null).then((function() {
                            i.Log.info("UserManager.removeUser: user removed from storage"),
                            t._events.unload()
                        }
                        ))
                    }
                    ,
                    e.prototype.signinRedirect = function() {
                        var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        (t = Object.assign({}, t)).request_type = "si:r";
                        var e = {
                            useReplaceToNavigate: t.useReplaceToNavigate
                        };
                        return this._signinStart(t, this._redirectNavigator, e).then((function() {
                            i.Log.info("UserManager.signinRedirect: successful")
                        }
                        ))
                    }
                    ,
                    e.prototype.signinRedirectCallback = function(t) {
                        return this._signinEnd(t || this._redirectNavigator.url).then((function(t) {
                            return t.profile && t.profile.sub ? i.Log.info("UserManager.signinRedirectCallback: successful, signed in sub: ", t.profile.sub) : i.Log.info("UserManager.signinRedirectCallback: no sub"),
                            t
                        }
                        ))
                    }
                    ,
                    e.prototype.signinPopup = function() {
                        var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        (t = Object.assign({}, t)).request_type = "si:p";
                        var e = t.redirect_uri || this.settings.popup_redirect_uri || this.settings.redirect_uri;
                        return e ? (t.redirect_uri = e,
                        t.display = "popup",
                        this._signin(t, this._popupNavigator, {
                            startUrl: e,
                            popupWindowFeatures: t.popupWindowFeatures || this.settings.popupWindowFeatures,
                            popupWindowTarget: t.popupWindowTarget || this.settings.popupWindowTarget
                        }).then((function(t) {
                            return t && (t.profile && t.profile.sub ? i.Log.info("UserManager.signinPopup: signinPopup successful, signed in sub: ", t.profile.sub) : i.Log.info("UserManager.signinPopup: no sub")),
                            t
                        }
                        ))) : (i.Log.error("UserManager.signinPopup: No popup_redirect_uri or redirect_uri configured"),
                        Promise.reject(new Error("No popup_redirect_uri or redirect_uri configured")))
                    }
                    ,
                    e.prototype.signinPopupCallback = function(t) {
                        return this._signinCallback(t, this._popupNavigator).then((function(t) {
                            return t && (t.profile && t.profile.sub ? i.Log.info("UserManager.signinPopupCallback: successful, signed in sub: ", t.profile.sub) : i.Log.info("UserManager.signinPopupCallback: no sub")),
                            t
                        }
                        )).catch((function(t) {
                            i.Log.error(t.message)
                        }
                        ))
                    }
                    ,
                    e.prototype.signinSilent = function() {
                        throw new Error("No thanks");
                        var t = this
                          , e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        return e = Object.assign({}, e),
                        this._loadUser().then((function(r) {
                            return r && r.refresh_token ? (e.refresh_token = r.refresh_token,
                            t._useRefreshToken(e)) : (e.request_type = "si:s",
                            e.id_token_hint = e.id_token_hint || t.settings.includeIdTokenInSilentRenew && r && r.id_token,
                            r && t._settings.validateSubOnSilentRenew && (i.Log.debug("UserManager.signinSilent, subject prior to silent renew: ", r.profile.sub),
                            e.current_sub = r.profile.sub),
                            t._signinSilentIframe(e))
                        }
                        ))
                    }
                    ,
                    e.prototype._useRefreshToken = function() {
                        var t = this
                          , e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        return this._tokenClient.exchangeRefreshToken(e).then((function(e) {
                            return e ? e.access_token ? t._loadUser().then((function(r) {
                                if (r) {
                                    var n = Promise.resolve();
                                    return e.id_token && (n = t._validateIdTokenFromTokenRefreshToken(r.profile, e.id_token)),
                                    n.then((function() {
                                        return i.Log.debug("UserManager._useRefreshToken: refresh token response success"),
                                        r.id_token = e.id_token || r.id_token,
                                        r.access_token = e.access_token,
                                        r.refresh_token = e.refresh_token || r.refresh_token,
                                        r.expires_in = e.expires_in,
                                        t.storeUser(r).then((function() {
                                            return t._events.load(r),
                                            r
                                        }
                                        ))
                                    }
                                    ))
                                }
                                return null
                            }
                            )) : (i.Log.error("UserManager._useRefreshToken: No access token returned from token endpoint"),
                            Promise.reject("No access token returned from token endpoint")) : (i.Log.error("UserManager._useRefreshToken: No response returned from token endpoint"),
                            Promise.reject("No response returned from token endpoint"))
                        }
                        ))
                    }
                    ,
                    e.prototype._validateIdTokenFromTokenRefreshToken = function(t, e) {
                        var r = this;
                        return this._metadataService.getIssuer().then((function(n) {
                            return r.settings.getEpochTime().then((function(o) {
                                return r._joseUtil.validateJwtAttributes(e, n, r._settings.client_id, r._settings.clockSkew, o).then((function(e) {
                                    return e ? e.sub !== t.sub ? (i.Log.error("UserManager._validateIdTokenFromTokenRefreshToken: sub in id_token does not match current sub"),
                                    Promise.reject(new Error("sub in id_token does not match current sub"))) : e.auth_time && e.auth_time !== t.auth_time ? (i.Log.error("UserManager._validateIdTokenFromTokenRefreshToken: auth_time in id_token does not match original auth_time"),
                                    Promise.reject(new Error("auth_time in id_token does not match original auth_time"))) : e.azp && e.azp !== t.azp ? (i.Log.error("UserManager._validateIdTokenFromTokenRefreshToken: azp in id_token does not match original azp"),
                                    Promise.reject(new Error("azp in id_token does not match original azp"))) : !e.azp && t.azp ? (i.Log.error("UserManager._validateIdTokenFromTokenRefreshToken: azp not in id_token, but present in original id_token"),
                                    Promise.reject(new Error("azp not in id_token, but present in original id_token"))) : void 0 : (i.Log.error("UserManager._validateIdTokenFromTokenRefreshToken: Failed to validate id_token"),
                                    Promise.reject(new Error("Failed to validate id_token")))
                                }
                                ))
                            }
                            ))
                        }
                        ))
                    }
                    ,
                    e.prototype._signinSilentIframe = function() {
                        var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , e = t.redirect_uri || this.settings.silent_redirect_uri || this.settings.redirect_uri;
                        return e ? (t.redirect_uri = e,
                        t.prompt = t.prompt || "none",
                        this._signin(t, this._iframeNavigator, {
                            startUrl: e,
                            silentRequestTimeout: t.silentRequestTimeout || this.settings.silentRequestTimeout
                        }).then((function(t) {
                            return t && (t.profile && t.profile.sub ? i.Log.info("UserManager.signinSilent: successful, signed in sub: ", t.profile.sub) : i.Log.info("UserManager.signinSilent: no sub")),
                            t
                        }
                        ))) : (i.Log.error("UserManager.signinSilent: No silent_redirect_uri configured"),
                        Promise.reject(new Error("No silent_redirect_uri configured")))
                    }
                    ,
                    e.prototype.signinSilentCallback = function(t) {
                        return this._signinCallback(t, this._iframeNavigator).then((function(t) {
                            return t && (t.profile && t.profile.sub ? i.Log.info("UserManager.signinSilentCallback: successful, signed in sub: ", t.profile.sub) : i.Log.info("UserManager.signinSilentCallback: no sub")),
                            t
                        }
                        ))
                    }
                    ,
                    e.prototype.signinCallback = function(t) {
                        var e = this;
                        return this.readSigninResponseState(t).then((function(r) {
                            var n = r.state;
                            return r.response,
                            "si:r" === n.request_type ? e.signinRedirectCallback(t) : "si:p" === n.request_type ? e.signinPopupCallback(t) : "si:s" === n.request_type ? e.signinSilentCallback(t) : Promise.reject(new Error("invalid response_type in state"))
                        }
                        ))
                    }
                    ,
                    e.prototype.signoutCallback = function(t, e) {
                        var r = this;
                        return this.readSignoutResponseState(t).then((function(n) {
                            var i = n.state
                              , o = n.response;
                            return i ? "so:r" === i.request_type ? r.signoutRedirectCallback(t) : "so:p" === i.request_type ? r.signoutPopupCallback(t, e) : Promise.reject(new Error("invalid response_type in state")) : o
                        }
                        ))
                    }
                    ,
                    e.prototype.querySessionStatus = function() {
                        var t = this
                          , e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        (e = Object.assign({}, e)).request_type = "si:s";
                        var r = e.redirect_uri || this.settings.silent_redirect_uri || this.settings.redirect_uri;
                        return r ? (e.redirect_uri = r,
                        e.prompt = "none",
                        e.response_type = e.response_type || this.settings.query_status_response_type,
                        e.scope = e.scope || "openid",
                        e.skipUserInfo = !0,
                        this._signinStart(e, this._iframeNavigator, {
                            startUrl: r,
                            silentRequestTimeout: e.silentRequestTimeout || this.settings.silentRequestTimeout
                        }).then((function(e) {
                            return t.processSigninResponse(e.url).then((function(t) {
                                if (i.Log.debug("UserManager.querySessionStatus: got signin response"),
                                t.session_state && t.profile.sub)
                                    return i.Log.info("UserManager.querySessionStatus: querySessionStatus success for sub: ", t.profile.sub),
                                    {
                                        session_state: t.session_state,
                                        sub: t.profile.sub,
                                        sid: t.profile.sid
                                    };
                                i.Log.info("querySessionStatus successful, user not authenticated")
                            }
                            )).catch((function(e) {
                                if (e.session_state && t.settings.monitorAnonymousSession && ("login_required" == e.message || "consent_required" == e.message || "interaction_required" == e.message || "account_selection_required" == e.message))
                                    return i.Log.info("UserManager.querySessionStatus: querySessionStatus success for anonymous user"),
                                    {
                                        session_state: e.session_state
                                    };
                                throw e
                            }
                            ))
                        }
                        ))) : (i.Log.error("UserManager.querySessionStatus: No silent_redirect_uri configured"),
                        Promise.reject(new Error("No silent_redirect_uri configured")))
                    }
                    ,
                    e.prototype._signin = function(t, e) {
                        var r = this
                          , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : {};
                        return this._signinStart(t, e, n).then((function(e) {
                            return r._signinEnd(e.url, t)
                        }
                        ))
                    }
                    ,
                    e.prototype._signinStart = function(t, e) {
                        var r = this
                          , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : {};
                        return e.prepare(n).then((function(e) {
                            return i.Log.debug("UserManager._signinStart: got navigator window handle"),
                            r.createSigninRequest(t).then((function(t) {
                                return i.Log.debug("UserManager._signinStart: got signin request"),
                                n.url = t.url,
                                n.id = t.state.id,
                                e.navigate(n)
                            }
                            )).catch((function(t) {
                                throw e.close && (i.Log.debug("UserManager._signinStart: Error after preparing navigator, closing navigator window"),
                                e.close()),
                                t
                            }
                            ))
                        }
                        ))
                    }
                    ,
                    e.prototype._signinEnd = function(t) {
                        var e = this
                          , r = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : {};
                        return this.processSigninResponse(t).then((function(t) {
                            i.Log.debug("UserManager._signinEnd: got signin response");
                            var n = new a.User(t);
                            if (r.current_sub) {
                                if (r.current_sub !== n.profile.sub)
                                    return i.Log.debug("UserManager._signinEnd: current user does not match user returned from signin. sub from signin: ", n.profile.sub),
                                    Promise.reject(new Error("login_required"));
                                i.Log.debug("UserManager._signinEnd: current user matches user returned from signin")
                            }
                            return e.storeUser(n).then((function() {
                                return i.Log.debug("UserManager._signinEnd: user stored"),
                                e._events.load(n),
                                n
                            }
                            ))
                        }
                        ))
                    }
                    ,
                    e.prototype._signinCallback = function(t, e) {
                        i.Log.debug("UserManager._signinCallback");
                        var r = "query" === this._settings.response_mode || !this._settings.response_mode && l.SigninRequest.isCode(this._settings.response_type) ? "?" : "#";
                        return e.callback(t, void 0, r)
                    }
                    ,
                    e.prototype.signoutRedirect = function() {
                        var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        (t = Object.assign({}, t)).request_type = "so:r";
                        var e = t.post_logout_redirect_uri || this.settings.post_logout_redirect_uri;
                        e && (t.post_logout_redirect_uri = e);
                        var r = {
                            useReplaceToNavigate: t.useReplaceToNavigate
                        };
                        return this._signoutStart(t, this._redirectNavigator, r).then((function() {
                            i.Log.info("UserManager.signoutRedirect: successful")
                        }
                        ))
                    }
                    ,
                    e.prototype.signoutRedirectCallback = function(t) {
                        return this._signoutEnd(t || this._redirectNavigator.url).then((function(t) {
                            return i.Log.info("UserManager.signoutRedirectCallback: successful"),
                            t
                        }
                        ))
                    }
                    ,
                    e.prototype.signoutPopup = function() {
                        var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                        (t = Object.assign({}, t)).request_type = "so:p";
                        var e = t.post_logout_redirect_uri || this.settings.popup_post_logout_redirect_uri || this.settings.post_logout_redirect_uri;
                        return t.post_logout_redirect_uri = e,
                        t.display = "popup",
                        t.post_logout_redirect_uri && (t.state = t.state || {}),
                        this._signout(t, this._popupNavigator, {
                            startUrl: e,
                            popupWindowFeatures: t.popupWindowFeatures || this.settings.popupWindowFeatures,
                            popupWindowTarget: t.popupWindowTarget || this.settings.popupWindowTarget
                        }).then((function() {
                            i.Log.info("UserManager.signoutPopup: successful")
                        }
                        ))
                    }
                    ,
                    e.prototype.signoutPopupCallback = function(t, e) {
                        return void 0 === e && "boolean" == typeof t && (e = t,
                        t = null),
                        this._popupNavigator.callback(t, e, "?").then((function() {
                            i.Log.info("UserManager.signoutPopupCallback: successful")
                        }
                        ))
                    }
                    ,
                    e.prototype._signout = function(t, e) {
                        var r = this
                          , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : {};
                        return this._signoutStart(t, e, n).then((function(t) {
                            return r._signoutEnd(t.url)
                        }
                        ))
                    }
                    ,
                    e.prototype._signoutStart = function() {
                        var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , e = this
                          , r = arguments[1]
                          , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : {};
                        return r.prepare(n).then((function(r) {
                            return i.Log.debug("UserManager._signoutStart: got navigator window handle"),
                            e._loadUser().then((function(o) {
                                return i.Log.debug("UserManager._signoutStart: loaded current user from storage"),
                                (e._settings.revokeAccessTokenOnSignout ? e._revokeInternal(o) : Promise.resolve()).then((function() {
                                    var s = t.id_token_hint || o && o.id_token;
                                    return s && (i.Log.debug("UserManager._signoutStart: Setting id_token into signout request"),
                                    t.id_token_hint = s),
                                    e.removeUser().then((function() {
                                        return i.Log.debug("UserManager._signoutStart: user removed, creating signout request"),
                                        e.createSignoutRequest(t).then((function(t) {
                                            return i.Log.debug("UserManager._signoutStart: got signout request"),
                                            n.url = t.url,
                                            t.state && (n.id = t.state.id),
                                            r.navigate(n)
                                        }
                                        ))
                                    }
                                    ))
                                }
                                ))
                            }
                            )).catch((function(t) {
                                throw r.close && (i.Log.debug("UserManager._signoutStart: Error after preparing navigator, closing navigator window"),
                                r.close()),
                                t
                            }
                            ))
                        }
                        ))
                    }
                    ,
                    e.prototype._signoutEnd = function(t) {
                        return this.processSignoutResponse(t).then((function(t) {
                            return i.Log.debug("UserManager._signoutEnd: got signout response"),
                            t
                        }
                        ))
                    }
                    ,
                    e.prototype.revokeAccessToken = function() {
                        var t = this;
                        return this._loadUser().then((function(e) {
                            return t._revokeInternal(e, !0).then((function(r) {
                                if (r)
                                    return i.Log.debug("UserManager.revokeAccessToken: removing token properties from user and re-storing"),
                                    e.access_token = null,
                                    e.refresh_token = null,
                                    e.expires_at = null,
                                    e.token_type = null,
                                    t.storeUser(e).then((function() {
                                        i.Log.debug("UserManager.revokeAccessToken: user stored"),
                                        t._events.load(e)
                                    }
                                    ))
                            }
                            ))
                        }
                        )).then((function() {
                            i.Log.info("UserManager.revokeAccessToken: access token revoked successfully")
                        }
                        ))
                    }
                    ,
                    e.prototype._revokeInternal = function(t, e) {
                        var r = this;
                        if (t) {
                            var n = t.access_token
                              , o = t.refresh_token;
                            return this._revokeAccessTokenInternal(n, e).then((function(t) {
                                return r._revokeRefreshTokenInternal(o, e).then((function(e) {
                                    return t || e || i.Log.debug("UserManager.revokeAccessToken: no need to revoke due to no token(s), or JWT format"),
                                    t || e
                                }
                                ))
                            }
                            ))
                        }
                        return Promise.resolve(!1)
                    }
                    ,
                    e.prototype._revokeAccessTokenInternal = function(t, e) {
                        return !t || t.indexOf(".") >= 0 ? Promise.resolve(!1) : this._tokenRevocationClient.revoke(t, e).then((function() {
                            return !0
                        }
                        ))
                    }
                    ,
                    e.prototype._revokeRefreshTokenInternal = function(t, e) {
                        return t ? this._tokenRevocationClient.revoke(t, e, "refresh_token").then((function() {
                            return !0
                        }
                        )) : Promise.resolve(!1)
                    }
                    ,
                    e.prototype.startSilentRenew = function() {
                        this._silentRenewService.start()
                    }
                    ,
                    e.prototype.stopSilentRenew = function() {
                        this._silentRenewService.stop()
                    }
                    ,
                    e.prototype._loadUser = function() {
                        return this._userStore.get(this._userStoreKey).then((function(t) {
                            return t ? (i.Log.debug("UserManager._loadUser: user storageString loaded"),
                            a.User.fromStorageString(t)) : (i.Log.debug("UserManager._loadUser: no user storageString"),
                            null)
                        }
                        ))
                    }
                    ,
                    e.prototype.storeUser = function(t) {
                        if (t) {
                            i.Log.debug("UserManager.storeUser: storing user");
                            var e = t.toStorageString();
                            return this._userStore.set(this._userStoreKey, e)
                        }
                        return i.Log.debug("storeUser.storeUser: removing user"),
                        this._userStore.remove(this._userStoreKey)
                    }
                    ,
                    n(e, [{
                        key: "_redirectNavigator",
                        get: function() {
                            return this.settings.redirectNavigator
                        }
                    }, {
                        key: "_popupNavigator",
                        get: function() {
                            return this.settings.popupNavigator
                        }
                    }, {
                        key: "_iframeNavigator",
                        get: function() {
                            return this.settings.iframeNavigator
                        }
                    }, {
                        key: "_userStore",
                        get: function() {
                            return this.settings.userStore
                        }
                    }, {
                        key: "events",
                        get: function() {
                            return this._events
                        }
                    }, {
                        key: "_userStoreKey",
                        get: function() {
                            return "user:" + this.settings.authority + ":" + this.settings.client_id
                        }
                    }]),
                    e
                }(o.OidcClient)
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.UserManagerSettings = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = (r(0),
                r(5))
                  , o = r(40)
                  , s = r(41)
                  , a = r(43)
                  , u = r(6)
                  , c = r(1)
                  , h = r(8);
                function l(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                function f(t, e) {
                    if (!t)
                        throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                    return !e || "object" != typeof e && "function" != typeof e ? t : e
                }
                e.UserManagerSettings = function(t) {
                    function e() {
                        var r = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {}
                          , n = r.popup_redirect_uri
                          , i = r.popup_post_logout_redirect_uri
                          , g = r.popupWindowFeatures
                          , d = r.popupWindowTarget
                          , p = r.silent_redirect_uri
                          , v = r.silentRequestTimeout
                          , y = r.automaticSilentRenew
                          , m = void 0 !== y && y
                          , _ = r.validateSubOnSilentRenew
                          , S = void 0 !== _ && _
                          , w = r.includeIdTokenInSilentRenew
                          , b = void 0 === w || w
                          , F = r.monitorSession
                          , E = void 0 === F || F
                          , x = r.monitorAnonymousSession
                          , A = void 0 !== x && x
                          , k = r.checkSessionInterval
                          , P = void 0 === k ? 2e3 : k
                          , C = r.stopCheckSessionOnError
                          , T = void 0 === C || C
                          , R = r.query_status_response_type
                          , I = r.revokeAccessTokenOnSignout
                          , D = void 0 !== I && I
                          , L = r.accessTokenExpiringNotificationTime
                          , N = void 0 === L ? 60 : L
                          , U = r.redirectNavigator
                          , O = void 0 === U ? new o.RedirectNavigator : U
                          , B = r.popupNavigator
                          , M = void 0 === B ? new s.PopupNavigator : B
                          , j = r.iframeNavigator
                          , H = void 0 === j ? new a.IFrameNavigator : j
                          , K = r.userStore
                          , V = void 0 === K ? new u.WebStorageStateStore({
                            store: c.Global.sessionStorage
                        }) : K;
                        l(this, e);
                        var q = f(this, t.call(this, arguments[0]));
                        return q._popup_redirect_uri = n,
                        q._popup_post_logout_redirect_uri = i,
                        q._popupWindowFeatures = g,
                        q._popupWindowTarget = d,
                        q._silent_redirect_uri = p,
                        q._silentRequestTimeout = v,
                        q._automaticSilentRenew = m,
                        q._validateSubOnSilentRenew = S,
                        q._includeIdTokenInSilentRenew = b,
                        q._accessTokenExpiringNotificationTime = N,
                        q._monitorSession = E,
                        q._monitorAnonymousSession = A,
                        q._checkSessionInterval = P,
                        q._stopCheckSessionOnError = T,
                        R ? q._query_status_response_type = R : arguments[0] && arguments[0].response_type ? q._query_status_response_type = h.SigninRequest.isOidc(arguments[0].response_type) ? "id_token" : "code" : q._query_status_response_type = "id_token",
                        q._revokeAccessTokenOnSignout = D,
                        q._redirectNavigator = O,
                        q._popupNavigator = M,
                        q._iframeNavigator = H,
                        q._userStore = V,
                        q
                    }
                    return function(t, e) {
                        if ("function" != typeof e && null !== e)
                            throw new TypeError("Super expression must either be null or a function, not " + typeof e);
                        t.prototype = Object.create(e && e.prototype, {
                            constructor: {
                                value: t,
                                enumerable: !1,
                                writable: !0,
                                configurable: !0
                            }
                        }),
                        e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
                    }(e, t),
                    n(e, [{
                        key: "popup_redirect_uri",
                        get: function() {
                            return this._popup_redirect_uri
                        }
                    }, {
                        key: "popup_post_logout_redirect_uri",
                        get: function() {
                            return this._popup_post_logout_redirect_uri
                        }
                    }, {
                        key: "popupWindowFeatures",
                        get: function() {
                            return this._popupWindowFeatures
                        }
                    }, {
                        key: "popupWindowTarget",
                        get: function() {
                            return this._popupWindowTarget
                        }
                    }, {
                        key: "silent_redirect_uri",
                        get: function() {
                            return this._silent_redirect_uri
                        }
                    }, {
                        key: "silentRequestTimeout",
                        get: function() {
                            return this._silentRequestTimeout
                        }
                    }, {
                        key: "automaticSilentRenew",
                        get: function() {
                            return this._automaticSilentRenew
                        }
                    }, {
                        key: "validateSubOnSilentRenew",
                        get: function() {
                            return this._validateSubOnSilentRenew
                        }
                    }, {
                        key: "includeIdTokenInSilentRenew",
                        get: function() {
                            return this._includeIdTokenInSilentRenew
                        }
                    }, {
                        key: "accessTokenExpiringNotificationTime",
                        get: function() {
                            return this._accessTokenExpiringNotificationTime
                        }
                    }, {
                        key: "monitorSession",
                        get: function() {
                            return this._monitorSession
                        }
                    }, {
                        key: "monitorAnonymousSession",
                        get: function() {
                            return this._monitorAnonymousSession
                        }
                    }, {
                        key: "checkSessionInterval",
                        get: function() {
                            return this._checkSessionInterval
                        }
                    }, {
                        key: "stopCheckSessionOnError",
                        get: function() {
                            return this._stopCheckSessionOnError
                        }
                    }, {
                        key: "query_status_response_type",
                        get: function() {
                            return this._query_status_response_type
                        }
                    }, {
                        key: "revokeAccessTokenOnSignout",
                        get: function() {
                            return this._revokeAccessTokenOnSignout
                        }
                    }, {
                        key: "redirectNavigator",
                        get: function() {
                            return this._redirectNavigator
                        }
                    }, {
                        key: "popupNavigator",
                        get: function() {
                            return this._popupNavigator
                        }
                    }, {
                        key: "iframeNavigator",
                        get: function() {
                            return this._iframeNavigator
                        }
                    }, {
                        key: "userStore",
                        get: function() {
                            return this._userStore
                        }
                    }]),
                    e
                }(i.OidcClientSettings)
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.RedirectNavigator = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0);
                e.RedirectNavigator = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t.prototype.prepare = function() {
                        return Promise.resolve(this)
                    }
                    ,
                    t.prototype.navigate = function(t) {
                        return t && t.url ? (t.useReplaceToNavigate ? window.location.replace(t.url) : window.location = t.url,
                        Promise.resolve()) : (i.Log.error("RedirectNavigator.navigate: No url provided"),
                        Promise.reject(new Error("No url provided")))
                    }
                    ,
                    n(t, [{
                        key: "url",
                        get: function() {
                            return window.location.href
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.PopupNavigator = void 0;
                var n = r(0)
                  , i = r(42);
                e.PopupNavigator = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t.prototype.prepare = function(t) {
                        var e = new i.PopupWindow(t);
                        return Promise.resolve(e)
                    }
                    ,
                    t.prototype.callback = function t(e, r, o) {
                        n.Log.debug("PopupNavigator.callback");
                        try {
                            return i.PopupWindow.notifyOpener(e, r, o),
                            Promise.resolve()
                        } catch (t) {
                            return Promise.reject(t)
                        }
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.PopupWindow = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0)
                  , o = r(3);
                e.PopupWindow = function() {
                    function t(e) {
                        var r = this;
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t),
                        this._promise = new Promise((function(t, e) {
                            r._resolve = t,
                            r._reject = e
                        }
                        ));
                        var n = e.popupWindowTarget || "_blank"
                          , o = e.popupWindowFeatures || "location=no,toolbar=no,width=500,height=500,left=100,top=100;";
                        this._popup = window.open("", n, o),
                        this._popup && (i.Log.debug("PopupWindow.ctor: popup successfully created"),
                        this._checkForPopupClosedTimer = window.setInterval(this._checkForPopupClosed.bind(this), 500))
                    }
                    return t.prototype.navigate = function(t) {
                        return this._popup ? t && t.url ? (i.Log.debug("PopupWindow.navigate: Setting URL in popup"),
                        this._id = t.id,
                        this._id && (window["popupCallback_" + t.id] = this._callback.bind(this)),
                        this._popup.focus(),
                        this._popup.window.location = t.url) : (this._error("PopupWindow.navigate: no url provided"),
                        this._error("No url provided")) : this._error("PopupWindow.navigate: Error opening popup window"),
                        this.promise
                    }
                    ,
                    t.prototype._success = function(t) {
                        i.Log.debug("PopupWindow.callback: Successful response from popup window"),
                        this._cleanup(),
                        this._resolve(t)
                    }
                    ,
                    t.prototype._error = function(t) {
                        i.Log.error("PopupWindow.error: ", t),
                        this._cleanup(),
                        this._reject(new Error(t))
                    }
                    ,
                    t.prototype.close = function() {
                        this._cleanup(!1)
                    }
                    ,
                    t.prototype._cleanup = function(t) {
                        i.Log.debug("PopupWindow.cleanup"),
                        window.clearInterval(this._checkForPopupClosedTimer),
                        this._checkForPopupClosedTimer = null,
                        delete window["popupCallback_" + this._id],
                        this._popup && !t && this._popup.close(),
                        this._popup = null
                    }
                    ,
                    t.prototype._checkForPopupClosed = function() {
                        this._popup && !this._popup.closed || this._error("Popup window closed")
                    }
                    ,
                    t.prototype._callback = function(t, e) {
                        this._cleanup(e),
                        t ? (i.Log.debug("PopupWindow.callback success"),
                        this._success({
                            url: t
                        })) : (i.Log.debug("PopupWindow.callback: Invalid response from popup"),
                        this._error("Invalid response from popup"))
                    }
                    ,
                    t.notifyOpener = function(t, e, r) {
                        if (window.opener) {
                            if (t = t || window.location.href) {
                                var n = o.UrlUtility.parseUrlFragment(t, r);
                                if (n.state) {
                                    var s = "popupCallback_" + n.state
                                      , a = window.opener[s];
                                    a ? (i.Log.debug("PopupWindow.notifyOpener: passing url message to opener"),
                                    a(t, e)) : i.Log.warn("PopupWindow.notifyOpener: no matching callback found on opener")
                                } else
                                    i.Log.warn("PopupWindow.notifyOpener: no state found in response url")
                            }
                        } else
                            i.Log.warn("PopupWindow.notifyOpener: no window.opener. Can't complete notification.")
                    }
                    ,
                    n(t, [{
                        key: "promise",
                        get: function() {
                            return this._promise
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.IFrameNavigator = void 0;
                var n = r(0)
                  , i = r(44);
                e.IFrameNavigator = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t.prototype.prepare = function(t) {
                        var e = new i.IFrameWindow(t);
                        return Promise.resolve(e)
                    }
                    ,
                    t.prototype.callback = function t(e) {
                        n.Log.debug("IFrameNavigator.callback");
                        try {
                            return i.IFrameWindow.notifyParent(e),
                            Promise.resolve()
                        } catch (t) {
                            return Promise.reject(t)
                        }
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.IFrameWindow = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0);
                e.IFrameWindow = function() {
                    function t(e) {
                        var r = this;
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t),
                        this._promise = new Promise((function(t, e) {
                            r._resolve = t,
                            r._reject = e
                        }
                        )),
                        this._boundMessageEvent = this._message.bind(this),
                        window.addEventListener("message", this._boundMessageEvent, !1),
                        this._frame = window.document.createElement("iframe"),
                        this._frame.style.visibility = "hidden",
                        this._frame.style.position = "absolute",
                        this._frame.width = 0,
                        this._frame.height = 0,
                        window.document.body.appendChild(this._frame)
                    }
                    return t.prototype.navigate = function(t) {
                        if (t && t.url) {
                            var e = t.silentRequestTimeout || 1e4;
                            i.Log.debug("IFrameWindow.navigate: Using timeout of:", e),
                            this._timer = window.setTimeout(this._timeout.bind(this), e),
                            this._frame.src = t.url
                        } else
                            this._error("No url provided");
                        return this.promise
                    }
                    ,
                    t.prototype._success = function(t) {
                        this._cleanup(),
                        i.Log.debug("IFrameWindow: Successful response from frame window"),
                        this._resolve(t)
                    }
                    ,
                    t.prototype._error = function(t) {
                        this._cleanup(),
                        i.Log.error(t),
                        this._reject(new Error(t))
                    }
                    ,
                    t.prototype.close = function() {
                        this._cleanup()
                    }
                    ,
                    t.prototype._cleanup = function() {
                        this._frame && (i.Log.debug("IFrameWindow: cleanup"),
                        window.removeEventListener("message", this._boundMessageEvent, !1),
                        window.clearTimeout(this._timer),
                        window.document.body.removeChild(this._frame),
                        this._timer = null,
                        this._frame = null,
                        this._boundMessageEvent = null)
                    }
                    ,
                    t.prototype._timeout = function() {
                        i.Log.debug("IFrameWindow.timeout"),
                        this._error("Frame window timed out")
                    }
                    ,
                    t.prototype._message = function(t) {
                        if (i.Log.debug("IFrameWindow.message"),
                        this._timer && t.origin === this._origin && t.source === this._frame.contentWindow && "string" == typeof t.data && (t.data.startsWith("http://") || t.data.startsWith("https://"))) {
                            var e = t.data;
                            e ? this._success({
                                url: e
                            }) : this._error("Invalid response from frame")
                        }
                    }
                    ,
                    t.notifyParent = function(t) {
                        i.Log.debug("IFrameWindow.notifyParent"),
                        (t = t || window.location.href) && (i.Log.debug("IFrameWindow.notifyParent: posting url message to parent"),
                        window.parent.postMessage(t, location.protocol + "//" + location.host))
                    }
                    ,
                    n(t, [{
                        key: "promise",
                        get: function() {
                            return this._promise
                        }
                    }, {
                        key: "_origin",
                        get: function() {
                            return location.protocol + "//" + location.host
                        }
                    }]),
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.UserManagerEvents = void 0;
                var n = r(0)
                  , i = r(16)
                  , o = r(17);
                e.UserManagerEvents = function(t) {
                    function e(r) {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, e);
                        var n = function(t, e) {
                            if (!t)
                                throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                            return !e || "object" != typeof e && "function" != typeof e ? t : e
                        }(this, t.call(this, r));
                        return n._userLoaded = new o.Event("User loaded"),
                        n._userUnloaded = new o.Event("User unloaded"),
                        n._silentRenewError = new o.Event("Silent renew error"),
                        n._userSignedIn = new o.Event("User signed in"),
                        n._userSignedOut = new o.Event("User signed out"),
                        n._userSessionChanged = new o.Event("User session changed"),
                        n
                    }
                    return function(t, e) {
                        if ("function" != typeof e && null !== e)
                            throw new TypeError("Super expression must either be null or a function, not " + typeof e);
                        t.prototype = Object.create(e && e.prototype, {
                            constructor: {
                                value: t,
                                enumerable: !1,
                                writable: !0,
                                configurable: !0
                            }
                        }),
                        e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
                    }(e, t),
                    e.prototype.load = function(e) {
                        var r = !(arguments.length > 1 && void 0 !== arguments[1]) || arguments[1];
                        n.Log.debug("UserManagerEvents.load"),
                        t.prototype.load.call(this, e),
                        r && this._userLoaded.raise(e)
                    }
                    ,
                    e.prototype.unload = function() {
                        n.Log.debug("UserManagerEvents.unload"),
                        t.prototype.unload.call(this),
                        this._userUnloaded.raise()
                    }
                    ,
                    e.prototype.addUserLoaded = function(t) {
                        this._userLoaded.addHandler(t)
                    }
                    ,
                    e.prototype.removeUserLoaded = function(t) {
                        this._userLoaded.removeHandler(t)
                    }
                    ,
                    e.prototype.addUserUnloaded = function(t) {
                        this._userUnloaded.addHandler(t)
                    }
                    ,
                    e.prototype.removeUserUnloaded = function(t) {
                        this._userUnloaded.removeHandler(t)
                    }
                    ,
                    e.prototype.addSilentRenewError = function(t) {
                        this._silentRenewError.addHandler(t)
                    }
                    ,
                    e.prototype.removeSilentRenewError = function(t) {
                        this._silentRenewError.removeHandler(t)
                    }
                    ,
                    e.prototype._raiseSilentRenewError = function(t) {
                        n.Log.debug("UserManagerEvents._raiseSilentRenewError", t.message),
                        this._silentRenewError.raise(t)
                    }
                    ,
                    e.prototype.addUserSignedIn = function(t) {
                        this._userSignedIn.addHandler(t)
                    }
                    ,
                    e.prototype.removeUserSignedIn = function(t) {
                        this._userSignedIn.removeHandler(t)
                    }
                    ,
                    e.prototype._raiseUserSignedIn = function() {
                        n.Log.debug("UserManagerEvents._raiseUserSignedIn"),
                        this._userSignedIn.raise()
                    }
                    ,
                    e.prototype.addUserSignedOut = function(t) {
                        this._userSignedOut.addHandler(t)
                    }
                    ,
                    e.prototype.removeUserSignedOut = function(t) {
                        this._userSignedOut.removeHandler(t)
                    }
                    ,
                    e.prototype._raiseUserSignedOut = function() {
                        n.Log.debug("UserManagerEvents._raiseUserSignedOut"),
                        this._userSignedOut.raise()
                    }
                    ,
                    e.prototype.addUserSessionChanged = function(t) {
                        this._userSessionChanged.addHandler(t)
                    }
                    ,
                    e.prototype.removeUserSessionChanged = function(t) {
                        this._userSessionChanged.removeHandler(t)
                    }
                    ,
                    e.prototype._raiseUserSessionChanged = function() {
                        n.Log.debug("UserManagerEvents._raiseUserSessionChanged"),
                        this._userSessionChanged.raise()
                    }
                    ,
                    e
                }(i.AccessTokenEvents)
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.Timer = void 0;
                var n = function() {
                    function t(t, e) {
                        for (var r = 0; r < e.length; r++) {
                            var n = e[r];
                            n.enumerable = n.enumerable || !1,
                            n.configurable = !0,
                            "value"in n && (n.writable = !0),
                            Object.defineProperty(t, n.key, n)
                        }
                    }
                    return function(e, r, n) {
                        return r && t(e.prototype, r),
                        n && t(e, n),
                        e
                    }
                }()
                  , i = r(0)
                  , o = r(1)
                  , s = r(17);
                function a(t, e) {
                    if (!(t instanceof e))
                        throw new TypeError("Cannot call a class as a function")
                }
                function u(t, e) {
                    if (!t)
                        throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
                    return !e || "object" != typeof e && "function" != typeof e ? t : e
                }
                e.Timer = function(t) {
                    function e(r) {
                        var n = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : o.Global.timer
                          , i = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : void 0;
                        a(this, e);
                        var s = u(this, t.call(this, r));
                        return s._timer = n,
                        s._nowFunc = i || function() {
                            return Date.now() / 1e3
                        }
                        ,
                        s
                    }
                    return function(t, e) {
                        if ("function" != typeof e && null !== e)
                            throw new TypeError("Super expression must either be null or a function, not " + typeof e);
                        t.prototype = Object.create(e && e.prototype, {
                            constructor: {
                                value: t,
                                enumerable: !1,
                                writable: !0,
                                configurable: !0
                            }
                        }),
                        e && (Object.setPrototypeOf ? Object.setPrototypeOf(t, e) : t.__proto__ = e)
                    }(e, t),
                    e.prototype.init = function(t) {
                        t <= 0 && (t = 1),
                        t = parseInt(t);
                        var e = this.now + t;
                        if (this.expiration === e && this._timerHandle)
                            i.Log.debug("Timer.init timer " + this._name + " skipping initialization since already initialized for expiration:", this.expiration);
                        else {
                            this.cancel(),
                            i.Log.debug("Timer.init timer " + this._name + " for duration:", t),
                            this._expiration = e;
                            var r = 5;
                            t < r && (r = t),
                            this._timerHandle = this._timer.setInterval(this._callback.bind(this), 1e3 * r)
                        }
                    }
                    ,
                    e.prototype.cancel = function() {
                        this._timerHandle && (i.Log.debug("Timer.cancel: ", this._name),
                        this._timer.clearInterval(this._timerHandle),
                        this._timerHandle = null)
                    }
                    ,
                    e.prototype._callback = function() {
                        var e = this._expiration - this.now;
                        i.Log.debug("Timer.callback; " + this._name + " timer expires in:", e),
                        this._expiration <= this.now && (this.cancel(),
                        t.prototype.raise.call(this))
                    }
                    ,
                    n(e, [{
                        key: "now",
                        get: function() {
                            return parseInt(this._nowFunc())
                        }
                    }, {
                        key: "expiration",
                        get: function() {
                            return this._expiration
                        }
                    }]),
                    e
                }(s.Event)
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.SilentRenewService = void 0;
                var n = r(0);
                e.SilentRenewService = function() {
                    function t(e) {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t),
                        this._userManager = e
                    }
                    return t.prototype.start = function() {
                        this._callback || (this._callback = this._tokenExpiring.bind(this),
                        this._userManager.events.addAccessTokenExpiring(this._callback),
                        this._userManager.getUser().then((function(t) {}
                        )).catch((function(t) {
                            n.Log.error("SilentRenewService.start: Error from getUser:", t.message)
                        }
                        )))
                    }
                    ,
                    t.prototype.stop = function() {
                        this._callback && (this._userManager.events.removeAccessTokenExpiring(this._callback),
                        delete this._callback)
                    }
                    ,
                    t.prototype._tokenExpiring = function() {
                        var t = this;
                        this._userManager.signinSilent().then((function(t) {
                            n.Log.debug("SilentRenewService._tokenExpiring: Silent token renewal successful")
                        }
                        ), (function(e) {
                            n.Log.error("SilentRenewService._tokenExpiring: Error from signinSilent:", e.message),
                            t._userManager.events._raiseSilentRenewError(e)
                        }
                        ))
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.CordovaPopupNavigator = void 0;
                var n = r(21);
                e.CordovaPopupNavigator = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t.prototype.prepare = function(t) {
                        var e = new n.CordovaPopupWindow(t);
                        return Promise.resolve(e)
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.CordovaIFrameNavigator = void 0;
                var n = r(21);
                e.CordovaIFrameNavigator = function() {
                    function t() {
                        !function(t, e) {
                            if (!(t instanceof e))
                                throw new TypeError("Cannot call a class as a function")
                        }(this, t)
                    }
                    return t.prototype.prepare = function(t) {
                        t.popupWindowFeatures = "hidden=yes";
                        var e = new n.CordovaPopupWindow(t);
                        return Promise.resolve(e)
                    }
                    ,
                    t
                }()
            }
            , function(t, e, r) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                e.Version = "1.11.5"
            }
            ])
        }
        ,
        t.exports = e()
    },
    981: (t,e,r)=>{
        "use strict";
        e.kO = e.Pd = void 0;
        const n = r(671);
        var i, o;
        !function(t) {
            t.Success = "success",
            t.RequiresRedirect = "requiresRedirect"
        }(i = e.Pd || (e.Pd = {})),
        function(t) {
            t.Redirect = "redirect",
            t.Success = "success",
            t.Failure = "failure",
            t.OperationCompleted = "operationCompleted"
        }(o = e.kO || (e.kO = {}));
        class s {
            constructor(t) {
                this._userManager = t
            }
            async trySilentSignIn() {
                return this._intialSilentSignIn || (this._intialSilentSignIn = (async()=>{
                     try {
                         await this._userManager.signinSilent()
                    } catch (t) {}
                }
                )()),
                this._intialSilentSignIn
            }
            async getUser() {
                //PATCH: Ignore silent OIDC login as Cognito does not support Iframes
                // window.parent !== window || window.opener || window.frameElement || !this._userManager.settings.redirect_uri || location.href.startsWith(this._userManager.settings.redirect_uri) || await a.instance.trySilentSignIn();
                const t = await this._userManager.getUser();
                return t && t.profile
            }
            async getAccessToken(t) {
                const e = await this._userManager.getUser();
                if (function(t) {
                    return !(!t || !t.access_token || t.expired || !t.scopes)
                }(e) && function(t, e) {
                    const r = new Set(e);
                    if (t && t.scopes)
                        for (const e of t.scopes)
                            if (!r.has(e))
                                return !1;
                    return !0
                }(t, e.scopes))
                    return {
                        status: i.Success,
                        token: {
                            grantedScopes: e.scopes,
                            expires: r(e.expires_in),
                            value: e.access_token
                        }
                    };
                try {
                    const e = t && t.scopes ? {
                        scope: t.scopes.join(" ")
                    } : void 0
                      , n = await this._userManager.signinSilent(e);
                    return {
                        status: i.Success,
                        token: {
                            grantedScopes: n.scopes,
                            expires: r(n.expires_in),
                            value: n.access_token
                        }
                    }
                } catch (t) {
                    return {
                        status: i.RequiresRedirect
                    }
                }
                function r(t) {
                    const e = new Date;
                    return e.setTime(e.getTime() + 1e3 * t),
                    e
                }
            }
            async signIn(t) {
                try {
                    return await this._userManager.clearStaleState(),
                    await this._userManager.signinSilent(this.createArguments()),
                    this.success(t)
                } catch (e) {
                    try {
                        return await this._userManager.clearStaleState(),
                        await this._userManager.signinRedirect(this.createArguments(t)),
                        this.redirect()
                    } catch (t) {
                        return this.error(this.getExceptionMessage(t))
                    }
                }
            }
            async completeSignIn(t) {
                const e = await this.loginRequired(t)
                  , r = await this.stateExists(t);
                try {
                    const e = await this._userManager.signinCallback(t);
                    return window.self !== window.top ? this.operationCompleted() : this.success(e && e.state)
                } catch (t) {
                    return e || window.self !== window.top || !r ? this.operationCompleted() : this.error("There was an error signing in.")
                }
            }
            async signOut(t) {
                try {
                    return await this._userManager.metadataService.getEndSessionEndpoint() ? (await this._userManager.signoutRedirect(this.createArguments(t)),
                    this.redirect()) : (await this._userManager.removeUser(),
                    this.success(t))
                } catch (t) {
                    return this.error(this.getExceptionMessage(t))
                }
            }
            async completeSignOut(t) {
                try {
                    if (await this.stateExists(t)) {
                        const e = await this._userManager.signoutCallback(t);
                        return this.success(e && e.state)
                    }
                    return this.operationCompleted()
                } catch (t) {
                    return this.error(this.getExceptionMessage(t))
                }
            }
            getExceptionMessage(t) {
                return function(t) {
                    return t && t.error_description
                }(t) ? t.error_description : function(t) {
                    return t && t.message
                }(t) ? t.message : t.toString()
            }
            async stateExists(t) {
                const e = new URLSearchParams(new URL(t).search).get("state");
                return e && this._userManager.settings.stateStore ? await this._userManager.settings.stateStore.get(e) : void 0
            }
            async loginRequired(t) {
                const e = new URLSearchParams(new URL(t).search).get("error");
                return !(!e || !this._userManager.settings.stateStore) && "login_required" === await this._userManager.settings.stateStore.get(e)
            }
            createArguments(t) {
                return {
                    useReplaceToNavigate: !0,
                    data: t
                }
            }
            error(t) {
                return {
                    status: o.Failure,
                    errorMessage: t
                }
            }
            success(t) {
                return {
                    status: o.Success,
                    state: t
                }
            }
            redirect() {
                return {
                    status: o.Redirect
                }
            }
            operationCompleted() {
                return {
                    status: o.OperationCompleted
                }
            }
        }
        class a {
            static init(t) {
                return a._initialized || (a._initialized = a.initializeCore(t)),
                a._initialized
            }
            static handleCallback() {
                return a.initializeCore()
            }
            static async initializeCore(t) {
                const e = t || a.resolveCachedSettings();
                if (!t && e) {
                    const t = a.createUserManagerCore(e);
                    window.parent !== window && !window.opener && window.frameElement && t.settings.redirect_uri && location.href.startsWith(t.settings.redirect_uri) && (a.instance = new s(t),
                    a._initialized = (async()=>{
                        await a.instance.completeSignIn(location.href)
                    }
                    )())
                } else if (t) {
                    const e = await a.createUserManager(t);
                    a.instance = new s(e)
                }
            }
            static resolveCachedSettings() {
                const t = window.sessionStorage.getItem(`${a._infrastructureKey}.CachedAuthSettings`);
                return t ? JSON.parse(t) : void 0
            }
            static getUser() {
                return a.instance.getUser()
            }
            static getAccessToken(t) {
                return a.instance.getAccessToken(t)
            }
            static signIn(t) {
                return a.instance.signIn(t)
            }
            static async completeSignIn(t) {
                let e = this._pendingOperations[t];
                return e || (e = a.instance.completeSignIn(t),
                await e,
                delete this._pendingOperations[t]),
                e
            }
            static signOut(t) {
                return a.instance.signOut(t)
            }
            static async completeSignOut(t) {
                let e = this._pendingOperations[t];
                return e || (e = a.instance.completeSignOut(t),
                await e,
                delete this._pendingOperations[t]),
                e
            }
            static async createUserManager(t) {
                let e;
                if (function(t) {
                    return t.hasOwnProperty("configurationEndpoint")
                }(t)) {
                    const r = await fetch(t.configurationEndpoint);
                    if (!r.ok)
                        throw new Error(`Could not load settings from '${t.configurationEndpoint}'`);
                    e = await r.json()
                } else
                    t.scope || (t.scope = t.defaultScopes.join(" ")),
                    null === t.response_type && delete t.response_type,
                    e = t;
                return window.sessionStorage.setItem(`${a._infrastructureKey}.CachedAuthSettings`, JSON.stringify(e)),
                a.createUserManagerCore(e)
            }
            static createUserManagerCore(t) {
                const e = new n.UserManager(t);
                return e.events.addUserSignedOut((async()=>{
                    e.removeUser()
                }
                )),
                e
            }
        }
        a._infrastructureKey = "Microsoft.AspNetCore.Components.WebAssembly.Authentication",
        a._pendingOperations = {},
        a.handleCallback(),
        window.AuthenticationService = a
    }
},
e = {},
function r(n) {
    var i = e[n];
    if (void 0 !== i)
        return i.exports;
    var o = e[n] = {
        exports: {}
    };
    return t[n].call(o.exports, o, o.exports, r),
    o.exports
}(981);
