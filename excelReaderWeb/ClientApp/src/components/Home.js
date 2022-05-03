import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { employee: [], loading: true };
    }

    componentDidMount() {
        this.populateEmployeeData();
    }

    static renderEmployeeTable(employee) {
        return (
            <table className='table table-bordered' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Employee Number</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Employee Status</th>
                    </tr>
                </thead>
                <tbody>
                    {employee.map(employee =>
                        <tr key={employee.EmployeeNumber} style={employee.EmployeeStatus === 'Regular' ? { color: 'green' } : { color: 'yellow' }}>
                            <td>{employee.EmployeeNumber}</td>
                            <td>{employee.FirstName}</td>
                            <td>{employee.LastName}</td>
                            <td>{employee.EmployeeStatus}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderEmployeeTable(this.state.employee);

        return (
            <div>
                <h1 id="tabelLabel" >Employee</h1>
                {contents}
            </div>
        );
    }

    async populateEmployeeData() {
        const response = await fetch('excel');
        const data = await response.json();
        console.log(`data`,data)
        this.setState({ employee: data, loading: false });
    }
}
