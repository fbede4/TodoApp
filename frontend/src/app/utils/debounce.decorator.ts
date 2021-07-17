/* eslint-disable prefer-arrow/prefer-arrow-functions */
// Implementation details at https://engineering.datorama.com/be-aware-of-the-debounce-decorator-6fb24a6d8d5
import { debounce as debounceFn, DebounceSettings } from 'lodash-es';

/**
 * Decorator that creates a debounced function.
 *
 * Useful when you don't have an RxJS stream which could be
 * debounced.
 *
 * @param wait The number of milliseconds to delay
 * @param options lodash debounce options
 *
 * @see {@link https://lodash.com/docs/4.17.15#debounce}
 *
 */
export function debounce(wait: number = 0, options: DebounceSettings = {}) {
  return function (
    _target: any,
    _propertyKey: string,
    descriptor: PropertyDescriptor
  ) {
    const map = new WeakMap();
    const originalMethod = descriptor.value;

    descriptor.value = function (...params: any) {
      let debounced = map.get(this);
      if (!debounced) {
        debounced = debounceFn(originalMethod, wait, options).bind(this);
        map.set(this, debounced);
      }
      debounced(...params);
    };
    return descriptor;
  };
}
