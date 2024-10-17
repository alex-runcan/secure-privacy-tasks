import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BINARY_STRING_PATH } from '@api/constants/path.constants';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BinaryStringService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private httpClient: HttpClient) {}

  public validateBinaryString(input: string): Observable<string> {
    return this.httpClient.post<string>(
      `${this.apiBaseUrl}/${BINARY_STRING_PATH}`,
      {
        value: input,
      }
    );
  }
}
