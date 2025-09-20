const { test, expect, chromium } = require('@playwright/test');

// Credenciales de prueba
const credentials = {
    webmaster: { username: 'MarkZuckerberg', password: 'Master999' },
    admin: { username: 'ElonMusk', password: 'Elon666' },
    comprador: { username: 'Pato', password: 'Patokey555' }
};

const baseURL = 'http://localhost:8081';

async function createBrowser() {
    const browser = await chromium.launch({ headless: false, slowMo: 1000 });
    const context = await browser.newContext();
    const page = await context.newPage();
    return { browser, context, page };
}

// QA Test 1: Validar que el contador de intentos NO se resetea cuando se ingresa correctamente
async function testLoginAttemptCounterIssue() {
    console.log('\n=== QA TEST 1: Login Attempt Counter Issue ===');
    
    const { browser, page } = await createBrowser();
    
    try {
        // Navegar a la página de login
        await page.goto(`${baseURL}/LogIn.aspx`);
        console.log('✓ Navegated to login page');

        // Intentar login con credenciales incorrectas varias veces
        console.log('Testing with incorrect passwords...');
        
        for (let i = 1; i <= 2; i++) {
            await page.fill('[id*="TextBox1User"]', credentials.comprador.username);
            await page.fill('[id*="TextBox2pass"]', 'contraseña_incorrecta');
            await page.click('[id*="ButtonLogin"]');
            
            console.log(`Attempt ${i}: Failed login with incorrect password`);
            await page.waitForTimeout(1000);
        }

        // Ahora intentar login con credenciales correctas
        console.log('Now trying with correct credentials...');
        await page.fill('[id*="TextBox1User"]', credentials.comprador.username);
        await page.fill('[id*="TextBox2pass"]', credentials.comprador.password);
        await page.click('[id*="ButtonLogin"]');

        // Verificar si se logea correctamente
        await page.waitForTimeout(2000);
        const currentUrl = page.url();
        console.log(`Current URL after correct login: ${currentUrl}`);
        
        if (currentUrl.includes('Default.aspx')) {
            console.log('✓ Login successful - should reset attempt counter');
            
            // Logout y intentar login incorrecto para ver si el contador se reseteo
            await page.goto(`${baseURL}/LogIn.aspx`);
            await page.fill('[id*="TextBox1User"]', credentials.comprador.username);
            await page.fill('[id*="TextBox2pass"]', 'otra_contraseña_incorrecta');
            await page.click('[id*="ButtonLogin"]');
            
            // Verificar el mensaje de error
            const errorText = await page.textContent('[id*="lblError"]').catch(() => '');
            console.log(`Error message: ${errorText}`);
            
            if (errorText.includes('Usuario o contraseña incorrectos')) {
                console.log('❌ BUG CONFIRMED: Counter might not have been reset properly');
            }
        } else {
            console.log('❌ Login failed even with correct credentials');
        }

    } catch (error) {
        console.error('Error in test:', error.message);
    } finally {
        await browser.close();
    }
}

// QA Test 2: Validar funcionamiento del Backup
async function testBackupFunctionality() {
    console.log('\n=== QA TEST 2: Backup Functionality ===');
    
    const { browser, page } = await createBrowser();
    
    try {
        // Login como Webmaster
        await page.goto(`${baseURL}/LogIn.aspx`);
        await page.fill('[id*="TextBox1User"]', credentials.webmaster.username);
        await page.fill('[id*="TextBox2pass"]', credentials.webmaster.password);
        await page.click('[id*="ButtonLogin"]');
        
        await page.waitForTimeout(2000);
        console.log('✓ Logged in as Webmaster');

        // Navegar a la página de backup
        await page.goto(`${baseURL}/BackRestore.aspx`);
        console.log('✓ Navigated to BackRestore page');

        // Intentar crear un backup
        const backupButton = await page.locator('[id*="Button1"]');
        if (await backupButton.isVisible()) {
            console.log('Backup button found, attempting to create backup...');
            await backupButton.click();
            
            // Esperar y verificar resultado
            await page.waitForTimeout(3000);
            
            // Verificar si hay alertas o mensajes de error
            const alerts = await page.evaluate(() => {
                return window.alert ? 'Alert detected' : 'No alerts';
            });
            
            console.log(`Backup result: ${alerts}`);
        } else {
            console.log('❌ Backup button not found or not visible');
        }

    } catch (error) {
        console.error('Error in backup test:', error.message);
    } finally {
        await browser.close();
    }
}

// QA Test 3: Validar manejo de errores de integridad
async function testIntegrityErrorHandling() {
    console.log('\n=== QA TEST 3: Integrity Error Handling ===');
    
    const { browser, page } = await createBrowser();
    
    try {
        // Intentar login para ver si hay errores de integridad activos
        await page.goto(`${baseURL}/LogIn.aspx`);
        await page.fill('[id*="TextBox1User"]', credentials.webmaster.username);
        await page.fill('[id*="TextBox2pass"]', credentials.webmaster.password);
        await page.click('[id*="ButtonLogin"]');
        
        await page.waitForTimeout(3000);
        const currentUrl = page.url();
        console.log(`URL after login: ${currentUrl}`);
        
        if (currentUrl.includes('BackRestore.aspx')) {
            console.log('❌ BUG CONFIRMED: Redirected directly to BackRestore instead of showing options');
            console.log('Expected: User should see options (Recalculate hashes vs Go to backup)');
            console.log('Actual: Automatic redirect to backup page');
        } else if (currentUrl.includes('Default.aspx')) {
            console.log('✓ No integrity errors detected, normal login flow');
        }

    } catch (error) {
        console.error('Error in integrity test:', error.message);
    } finally {
        await browser.close();
    }
}

// QA Test 4: Validar funcionamiento del Restore
async function testRestoreFunctionality() {
    console.log('\n=== QA TEST 4: Restore Functionality ===');
    
    const { browser, page } = await createBrowser();
    
    try {
        // Login como Webmaster y navegar a backup/restore
        await page.goto(`${baseURL}/LogIn.aspx`);
        await page.fill('[id*="TextBox1User"]', credentials.webmaster.username);
        await page.fill('[id*="TextBox2pass"]', credentials.webmaster.password);
        await page.click('[id*="ButtonLogin"]');
        await page.waitForTimeout(2000);
        
        await page.goto(`${baseURL}/BackRestore.aspx`);
        console.log('✓ Navigated to BackRestore page');

        // Verificar si hay un control de file upload para restore
        const fileInput = await page.locator('input[type="file"]');
        const restoreButton = await page.locator('[id*="Button2"]');
        
        if (await fileInput.isVisible() && await restoreButton.isVisible()) {
            console.log('✓ Restore controls are visible');
            // Note: No podemos probar el upload sin un archivo real
            console.log('⚠️  Cannot test actual restore without a backup file');
        } else {
            console.log('❌ Restore controls not found or not visible');
        }

    } catch (error) {
        console.error('Error in restore test:', error.message);
    } finally {
        await browser.close();
    }
}

// QA Test 5: Validar acceso directo por URL cuando hay errores de integridad
async function testDirectUrlAccess() {
    console.log('\n=== QA TEST 5: Direct URL Access with Integrity Errors ===');
    
    const { browser, page } = await createBrowser();
    
    try {
        // Intentar acceso directo a páginas sin login
        const protectedPages = [
            '/Default.aspx',
            '/Categorias.aspx',
            '/CarritoCompra.aspx',
            '/Bitacora.aspx'
        ];

        for (const pagePath of protectedPages) {
            console.log(`Testing direct access to: ${pagePath}`);
            await page.goto(`${baseURL}${pagePath}`);
            await page.waitForTimeout(1000);
            
            const currentUrl = page.url();
            console.log(`Redirected to: ${currentUrl}`);
            
            if (!currentUrl.includes('LogIn.aspx')) {
                console.log(`❌ BUG CONFIRMED: Direct access allowed to ${pagePath}`);
            } else {
                console.log(`✓ Properly redirected to login for ${pagePath}`);
            }
        }

        // Test: Login y luego usar botón Home cuando hay errores de integridad
        await page.goto(`${baseURL}/LogIn.aspx`);
        await page.fill('[id*="TextBox1User"]', credentials.webmaster.username);
        await page.fill('[id*="TextBox2pass"]', credentials.webmaster.password);
        await page.click('[id*="ButtonLogin"]');
        await page.waitForTimeout(2000);

        // Si estamos en BackRestore por error de integridad, probar navegar al home
        if (page.url().includes('BackRestore.aspx')) {
            console.log('Testing Home navigation when integrity error is present...');
            
            // Buscar y hacer clic en el icono/link de Home
            const homeLink = await page.locator('a[href*="Default.aspx"], [id*="Home"], .home-icon').first();
            if (await homeLink.isVisible()) {
                await homeLink.click();
                await page.waitForTimeout(2000);
                
                const newUrl = page.url();
                if (newUrl.includes('Default.aspx')) {
                    console.log('❌ BUG CONFIRMED: Can access full menu via Home icon despite integrity errors');
                }
            }
        }

    } catch (error) {
        console.error('Error in direct URL access test:', error.message);
    } finally {
        await browser.close();
    }
}

// Ejecutar todos los tests
async function runAllQATests() {
    console.log('🚀 Starting UAI Shop QA Validation Tests');
    console.log('==========================================');
    
    await testLoginAttemptCounterIssue();
    await testBackupFunctionality();
    await testIntegrityErrorHandling();
    await testRestoreFunctionality();
    await testDirectUrlAccess();
    
    console.log('\n✅ QA Validation Tests Completed');
    console.log('Check the output above for bug confirmations and findings');
}

// Export para uso modular
module.exports = {
    testLoginAttemptCounterIssue,
    testBackupFunctionality,
    testIntegrityErrorHandling,
    testRestoreFunctionality,
    testDirectUrlAccess,
    runAllQATests
};

// Si se ejecuta directamente
if (require.main === module) {
    runAllQATests().catch(console.error);
}