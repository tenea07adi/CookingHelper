import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({providedIn: "root"})
export class AppConfigService {

    private httpClient = inject(HttpClient); 

    private configUrl = "./assets/appconfig.json";
    private configs: any = {};

    getRestApiUrl(): string {
        return this.configs["restApi"]["url"];
    }
     
    public async loadConfig(): Promise<any> {
        return this.httpClient
            .get(this.configUrl)
            .pipe((settings) => settings)
            .toPromise()
            .then((config) => {
                this.configs = config;
        });
    }
}