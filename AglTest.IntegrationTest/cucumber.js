module.exports = {
    default: [
        '--require-module ts-node/register',
        '--require ./features/**/*.ts',
        '--require ./step_definitions/**/*.ts',
    ].join(' '),
};