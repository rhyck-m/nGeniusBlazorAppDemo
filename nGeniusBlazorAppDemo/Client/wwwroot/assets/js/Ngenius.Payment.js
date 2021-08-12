// Ngenius Demo

AppNGenius = {
    // [1] Mount the card in the Dom
    mountPaymentCard: function (_ngeniusSessionID, __outletRef) {
        "use strict";
        const style = {
            main: {
                margin: 0,
                padding: '3px',
                background: '#90aab1',
                width: '100%'
            },
            base: {} /* this is the default styles that comes with it */,
            input: {
                padding: '.45rem .9rem',
                fontSize: '2rem',
                fontWeight: '400',
                backgroundColor: '#fff',
                borderWidth: '1px',
                borderColor: '#dee2e6',
                borderRadius: '.25rem'
            } /* custom style options for the input fields */,
            invalid: {} /* custom input invalid styles */
        };

        window.NI.mountCardInput('mount-id', {
            style: style,
            apiKey: _ngeniusSessionID,
            outletRef: __outletRef,
            onSuccess: ((res) => {
                console.log(res);
            }),
            onFail: ((err) => {
                console.log(err);
            }),
            onChangeValidStatus: (function (_ref) {
                var isCVVValid = _ref.isCVVValid,
                    isExpiryValid = _ref.isExpiryValid,
                    isNameValid = _ref.isNameValid,
                    isPanValid = _ref.isPanValid;
                console.log(isCVVValid, isExpiryValid, isNameValid, isPanValid);
            })
        });
    },

    // [2] Get Session ID
    getOrderSessionID: function () {
        return window.NI.generateSessionId().then((response) => {
            console.log(response);
            return response.session_id;
        }).catch(function (error) {
            console.log(error);
            return `Error :: ${error}`;
        });
    },

    // Optional, try to process Order with JavaScript
    processPayment: function (sessionID, accessToken, outletRef) {
        console.log("The sessionID :: ", sessionID);
        console.log("The outletRefID :: ", outletRef);
        const order = {
            "action": "SALE",
            "amount": { "currencyCode": "AED", "value": 300 }
        };

        return axios.post(
            `https://api-gateway.sandbox.ngenius-payments.com/transactions/outlets/${outletRef}/payment/hosted-session/${sessionID}`,
            order,
            {
                headers: {
                    'Authorization': `Bearer ${accessToken}`,
                    'Content-Type': 'application/vnd.ni-payment.v2+json',
                    'Accept': 'application/vnd.ni-payment.v2+json'
                }
            }
        )
            .then((paymentResponse) => {
                console.log(paymentResponse);
                /* paymentResponse is the response from the previous call */
                //return window.NI.handlePaymentResponse(paymentResponse).then((response) => {
                //    console.log("The Payment 3D Response :: ", response);
                //    console.log(response.status, response.error);
                //    return response.status;
                //});
            })
            .catch((err) => {
                console.log('Error posting payment :: ', err);
            })
    }
}

AppAlerts = {
    ShowSwalAlert: function (message, alertType) {
        return new Promise((resolve) => {
            swal({
                // title: title,
                text: message,
                // type: type,
                closeOnClickOutside: false,
                buttons: alertType === "question" ? { cancel: true, confirm: "OK" } : { cancel: false, confirm: "OK" },
                dangerMode: true
            }).then(function (action) {
                if (action) {
                    resolve(true);
                } else {
                    resolve(false);
                }
            });
        });
    }
};


