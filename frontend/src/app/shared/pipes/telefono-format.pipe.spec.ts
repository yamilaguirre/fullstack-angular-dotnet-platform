import { TelefonoFormatPipe } from './telefono-format.pipe';

describe('TelefonoFormatPipe', () => {
  let pipe: TelefonoFormatPipe;

  beforeEach(() => {
    pipe = new TelefonoFormatPipe();
  });

  it('formats Chile mobile sample with spaces every four characters', () => {
    expect(pipe.transform('+56912345678')).toBe('+569 1234 5678');
  });

  it('adds plus prefix when missing and strips non-digits', () => {
    expect(pipe.transform('569 12-34-56-78')).toBe('+569 1234 5678');
  });

  it('returns empty string for null or empty input', () => {
    expect(pipe.transform(null)).toBe('');
    expect(pipe.transform('')).toBe('');
  });
});
