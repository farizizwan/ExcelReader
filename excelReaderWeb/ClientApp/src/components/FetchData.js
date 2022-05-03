import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderForecastsTable(forecasts) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Employee Number</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Employee Status</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.EmployeeNumber} style={forecast.EmployeeStatus === 'Regular' ? { color: 'green' } : { color: 'yellow'}}>
                            <td>{forecast.EmployeeNumber}</td>
                            <td>{forecast.FirstName}</td>
                            <td>{forecast.LastName}</td>
                            <td>{forecast.EmployeeStatus}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        console.log(`data`, data)
        this.setState({ forecasts: data, loading: false });
    }
}
