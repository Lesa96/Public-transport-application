function initPaypalButton(divID,isLoggedIn,mail, price, ticketType) {

    if (price !== undefined) {
        if (!isLoggedIn) {
            paypal.Buttons({
                style: {
                    layout: 'horizontal',
                    color: 'blue',
                    shape: 'rect',
                    label: 'paypal',
                    size: 'small'
                },
                createOrder: function (data, actions) {
                    return actions.order.create({
                        purchase_units: [{
                            amount: {
                                value: price
                            }
                        }]
                    });
                },
                onApprove: function (data, actions) {
                    return actions.order.capture().then(function (details) {
                        alert('Transaction completed by ' + details.payer.name.given_name);
                        // Call your server to save the transaction
                        return fetch('http://localhost:8080/api/Ticket/BuyUnregistered', {
                            method: 'post',
                            headers: {
                                'content-type': 'application/json'
                            },
                            body: JSON.stringify({
                                email: mail,
                                orderId: data.orderID
                            })
                        });
                    });
                }
            }).render(`#paypal-button-container-${divID}`);

            

        } else {
            paypal.Buttons({
                style: {
                    layout: 'horizontal',
                    color: 'blue',
                    shape: 'rect',
                    label: 'paypal',
                    size: 'small'
                },
                createOrder: function (data, actions) {
                    return actions.order.create({
                        purchase_units: [{
                            amount: {
                                value: price
                            }
                        }]
                    });
                },
                onApprove: function (data, actions) {
                    return actions.order.capture().then(function (details) {
                        alert('Transaction completed by ' + details.payer.name.given_name);
                        // Call your server to save the transaction
                        return fetch('http://localhost:8080/api/Ticket/BuyTicket', {
                            method: 'post',
                            headers: {
                                'content-type': 'application/json'
                            },
                            body: JSON.stringify({
                                Email: mail,
                                TicketType: ticketType,
                                orderId: data.orderID
                            })
                        });
                    });
                }
            }).render(`#paypal-button-container-${divID}`);

            
        }

    }
}