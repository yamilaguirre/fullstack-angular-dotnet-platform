import { Pipe, PipeTransform } from '@angular/core';

/**
 * Formats telephone strings by normalizing to leading "+" + digits, then inserting a space every four characters.
 * Example: "+56912345678" → "+569 1234 5678".
 */
@Pipe({
  name: 'telefonoFormat',
  standalone: true
})
export class TelefonoFormatPipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (value == null || value === '') {
      return '';
    }

    const compact = value.replace(/\s+/g, '').trim();
    if (compact === '') {
      return '';
    }

    const digitsOnly = compact.replace(/\D/g, '');
    if (digitsOnly === '') {
      return compact;
    }

const withPlus = `+${digitsOnly}`;

    const chunks: string[] = [];
    for (let i = 0; i < withPlus.length; i += 4) {
      chunks.push(withPlus.slice(i, i + 4));
    }

    return chunks.join(' ');
  }
}
