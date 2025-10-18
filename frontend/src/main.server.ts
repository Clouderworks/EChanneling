import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { config } from './app/app.config.server';
import { provideServerRendering } from '@angular/platform-server';


export default function() {
	return bootstrapApplication(AppComponent, {
		providers: [
			provideServerRendering(),
			...config.providers
		]
	});
}
