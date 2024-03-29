from jupyter_plotly_dash import JupyterDash

import dash
import dash_leaflet as dl
import dash_core_components as dcc
import dash_html_components as html
import plotly.express as px
import dash_table
from dash.dependencies import Input, Output

import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from pymongo import MongoClient

#### FIX ME #####\n",
# change animal_shelter and AnimalShelter to match your CRUD Python module file name and class name\n",
from AnimalShelter import AnimalShelter

###########################
# Data Manipulation / Model
###########################
# FIX ME update with your username and password and CRUD Python module name

username = "aacuser"
password = "Password4"
shelter = AnimalShelter(username, password)

# class read method must support return of cursor object and accept projection json input
df = pd.DataFrame.from_records(shelter.read({}))

#########################
# Dashboard Layout / View
#########################
app = JupyterDash('SimpleExample')

app.layout = html.Div([
    html.Div(id='hidden-div', style={'display':'none'}),
    html.Center(html.B(html.H1('SNHU CS-340 Dashboard'))),
    html.Hr(),
    dash_table.DataTable(
        id='datatable-id',
        columns=[
            {"name": i.replace("_", " "), "id": i, "deletable": False, "selectable": True} for i in df.columns
        ],
        #FIXME: Set up the features for your interactive data table to make it user-friendly for your client
        sort_action= "native"
        filter_action= "native"
        data=df.to_dict('records'),
        page_size=25,
        fixed_rows={'headers': True},
        style_header={
            "height" : 60,
            "text-transform" : "uppercase"
        }
    ),
    html.Br(),
    html.Hr(),
    html.Div(
           id='map-id',
           className='col s12 m6',
           ),
    html.Hr(),
    html.Footer(
        html.Center(
            html.B('Michael Darling | CS-340 | Module 6 Milestone | 2021.12.05')
        )
    )
]);

#############################################
# Interaction Between Components / Controller
#############################################
#This callback will highlight a row on the data table when the user selects it\n",
@app.callback(
    Output('datatable-id', 'style_data_conditional'),
    [Input('datatable-id', 'selected_columns')]
)
def update_styles(selected_columns):
    if selected_columns is None:
        return
    return [{
        'if': { 'column_id': i },
        'background_color': '#D2F3FF'
    } for i in selected_columns]

@app.callback(
    Output('map-id', "children"),
    [Input('datatable-id', derived_viewport_data)])
def update_map(viewData):
    #FIXME Add in the code for your geolocation chart
    dff = pd.DataFrame.from_dict(viewData)
    return [
        dl.Map(style={'width': '1000px', 'height': '500px'}, center=[30.75,-97.48], zoom=10, children=[
            dl.TileLayer(id="base-layer-id"),
            # Marker with tool tip and popup\n",
            dl.Marker(position=[dff.iloc[1,13],dff.iloc[1,14]], children=[
                dl.Tooltip(dff.iloc[1,4]),\
                dl.Popup([\
                    html.H1("Animal Name"),
                    html.P(dff.iloc[1,9])
                ])
            ])
        ])
    ]

app